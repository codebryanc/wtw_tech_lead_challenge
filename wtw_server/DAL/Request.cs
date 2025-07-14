using DAL.Repositories;
using Entity;

namespace DAL
{
    public interface IRequest
    {
        Task<IEnumerable<Requests>> GetAllRequestsAsync();
    }

    public class Request : IRequest
    {
        private readonly IRequestRepository _requestRepository;

        public Request(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IEnumerable<Requests>> GetAllRequestsAsync()
        {
            return await _requestRepository.GetAllAsync();
        }
    }
}
