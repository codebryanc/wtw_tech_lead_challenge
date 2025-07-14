using System.Collections.Generic;
using System.Threading.Tasks;

using Entity;
using DAL.Base.UnitOfWork;

namespace DAL
{
    // [Interface]
    public interface IRequest
    {
        Task<IEnumerable<Requests>> GetAllRequestsAsync();
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
            return await _unitOfWork.Repository<Requests>().GetAllAsync();
        }
    }
}