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
                               }).ToListAsync();
            
            return result;
        }

        public async Task<IEnumerable<Requests>> GetFilteredRequestsAsync(RequestFilterDto filter)
        {
            var query = _unitOfWork.Repository<Requests>().AsQueryable();

            // Filter by valid JSON data first
            query = query.WhereValidJson();

            if (filter.RequestTypeId.HasValue)
                query = query.Where(r => r.rtyId == filter.RequestTypeId.Value);

            if (filter.RequestStatusId.HasValue)
                query = query.Where(r => r.resId == filter.RequestStatusId.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(r => r.createdAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(r => r.createdAt <= filter.ToDate.Value);

            // Use efficient JSON_VALUE function instead of string contains
            if (!string.IsNullOrEmpty(filter.JsonProperty) && !string.IsNullOrEmpty(filter.JsonValue))
            {
                query = query.WhereJsonProperty(filter.JsonProperty, filter.JsonValue);
            }

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

        public async Task<Requests> CreateRequestAsync(RequestCreateDto requestDto)
        {
            var request = new Requests
            {
                reqId = Guid.NewGuid(),
                rtyId = requestDto.RequestTypeId,
                resId = requestDto.RequestStatusId,
                createdAt = DateTime.UtcNow,
                data = JsonSerializer.Serialize(requestDto.DynamicData)
            };

            await _unitOfWork.Repository<Requests>().AddAsync(request);
            await _unitOfWork.SaveChangesAsync();
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