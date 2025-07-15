using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Entity;
using Entity.DTOs;
using DAL.Base.UnitOfWork;
using DAL.Extensions;

namespace DAL
{
    // [Interface]
    public interface IRequest
    {
        Task<IEnumerable<Requests>> GetAllRequestsAsync();
        Task<IEnumerable<Requests>> GetFilteredRequestsAsync(RequestFilterDto filter);
        Task<Requests> CreateRequestAsync(RequestCreateDto requestDto);
        Task<Requests?> GetRequestByIdAsync(Guid id);
        Task<bool> DeleteRequestAsync(Guid id);
        Task<IEnumerable<Requests>> SearchByJsonPropertyAsync(string propertyName, string value);
    }

    public class Request : IRequest
    {
        // [Properties]
        private readonly IUnitOfWork _unitOfWork;

        // [Constructor]
        public Request(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // [Methods]
        public async Task<IEnumerable<Requests>> GetAllRequestsAsync()
        {
            var query = _unitOfWork.Repository<Requests>().AsQueryable();
            
            var result = await (from r in query
                               join rt in _unitOfWork.Repository<RequestTypes>().AsQueryable() on r.rtyId equals rt.rtyId
                               join rs in _unitOfWork.Repository<RequestStatusEntity>().AsQueryable() on r.resId equals rs.resId
                               select new Requests
                               {
                                   reqId = r.reqId,
                                   rtyId = r.rtyId,
                                   resId = r.resId,
                                   createdAt = r.createdAt,
                                   data = r.data,
                                   requestTypeName = rt.name,
                                   requestStatusName = rs.name
                               })
                               .ToListAsync();
            
            return result;
        }

        public async Task<IEnumerable<Requests>> GetFilteredRequestsAsync(RequestFilterDto filter)
        {
            var query = _unitOfWork.Repository<Requests>().AsQueryable();

            // Apply filters without JSON validation first
            if (filter.RequestTypeId.HasValue)
                query = query.Where(r => r.rtyId == filter.RequestTypeId.Value);

            if (filter.RequestStatusId.HasValue)
                query = query.Where(r => r.resId == filter.RequestStatusId.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(r => r.createdAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(r => r.createdAt <= filter.ToDate.Value);

            // Apply JSON filter only if specified
            if (!string.IsNullOrEmpty(filter.JsonProperty) && !string.IsNullOrEmpty(filter.JsonValue))
            {
                query = query.Where(r => r.data != null).WhereJsonPropertyContains(filter.JsonProperty, filter.JsonValue);
            }

            IEnumerable<Requests> result = new List<Requests>();

            try {
                result = await (from r in query
                                    join rt in _unitOfWork.Repository<RequestTypes>().AsQueryable() on r.rtyId equals rt.rtyId
                                    join rs in _unitOfWork.Repository<RequestStatusEntity>().AsQueryable() on r.resId equals rs.resId
                                    select new Requests
                                    {
                                        reqId = r.reqId,
                                        rtyId = r.rtyId,
                                        resId = r.resId,
                                        createdAt = r.createdAt,
                                        data = r.data,
                                        requestTypeName = rt.name,
                                        requestStatusName = rs.name
                                    }).OrderByCreationDate(true).ToListAsync();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            return result;
        }

        public async Task<Requests> CreateRequestAsync(RequestCreateDto requestDto)
        {
            Requests request = new Requests();

            try
            {
                request = new Requests
                {
                    reqId = Guid.NewGuid(),
                    rtyId = Guid.Parse(requestDto.RequestTypeId != null ? requestDto.RequestTypeId! : Guid.Empty.ToString()),
                    resId = Guid.Parse(requestDto.RequestStatusId != null ? requestDto.RequestStatusId! : Guid.Empty.ToString()),
                    createdAt = DateTime.UtcNow,
                    data = JsonSerializer.Serialize(requestDto.DynamicData)
                };

                await _unitOfWork.Repository<Requests>().AddAsync(request);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            return request;
        }

        public async Task<Requests?> GetRequestByIdAsync(Guid id)
        {
            var result = await (from r in _unitOfWork.Repository<Requests>().AsQueryable()
                               join rt in _unitOfWork.Repository<RequestTypes>().AsQueryable() on r.rtyId equals rt.rtyId
                               join rs in _unitOfWork.Repository<RequestStatusEntity>().AsQueryable() on r.resId equals rs.resId
                               where r.reqId == id
                               select new Requests
                               {
                                   reqId = r.reqId,
                                   rtyId = r.rtyId,
                                   resId = r.resId,
                                   createdAt = r.createdAt,
                                   data = r.data,
                                   requestTypeName = rt.name,
                                   requestStatusName = rs.name
                               }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> DeleteRequestAsync(Guid id)
        {
            var request = await GetRequestByIdAsync(id);
            if (request == null) return false;

            _unitOfWork.Repository<Requests>().Remove(request);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Requests>> SearchByJsonPropertyAsync(string propertyName, string value)
        {
            var query = _unitOfWork.Repository<Requests>()
                .AsQueryable()
                .WhereValidJson()
                .WhereJsonPropertyContains(propertyName, value);

            var result = await (from r in query
                               join rt in _unitOfWork.Repository<RequestTypes>().AsQueryable() on r.rtyId equals rt.rtyId
                               join rs in _unitOfWork.Repository<RequestStatusEntity>().AsQueryable() on r.resId equals rs.resId
                               select new Requests
                               {
                                   reqId = r.reqId,
                                   rtyId = r.rtyId,
                                   resId = r.resId,
                                   createdAt = r.createdAt,
                                   data = r.data,
                                   requestTypeName = rt.name,
                                   requestStatusName = rs.name
                               }).OrderByCreationDate(true).ToListAsync();

            return result;
        }
    }
}