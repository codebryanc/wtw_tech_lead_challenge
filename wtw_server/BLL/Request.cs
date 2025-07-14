using Entity;

namespace BLL
{
    public interface IRequest
    {
        Task<List<Requests>> GetAllRequestsAsync();
    }

    public class Request : IRequest
    {
        private readonly DAL.IRequest _dalRequest;

        public Request(DAL.IRequest dalRequest)
        {
            _dalRequest = dalRequest;
        }

        public async Task<List<Requests>> GetAllRequestsAsync()
        {
            var result = await _dalRequest.GetAllRequestsAsync();
            return result.ToList();
        }
    }
}
