using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity;

namespace BLL
{
    public interface IRequest
    {
        // [Methods]
        Task<List<Requests>> GetAllRequestsAsync();
    }

    public class Request : IRequest
    {
        // [Properties]
        private readonly DAL.IRequest _dalRequest;

        // [Constructor]
        public Request(DAL.IRequest dalRequest)
        {
            _dalRequest = dalRequest;
        }

        // [Methods]
        public async Task<List<Requests>> GetAllRequestsAsync()
        {
            var result = await _dalRequest.GetAllRequestsAsync();
            return result.ToList();
        }
    }
}
