using Entity;

namespace BLL
{
    public interface IRequest
    {
        List<Requests> GetAllRequests();
    }

    public class Request : IRequest
    {
        private readonly DAL.IRequest _dalRequest;

        public Request(DAL.IRequest dalRequest)
        {
            _dalRequest = dalRequest;
        }

        public List<Requests> GetAllRequests()
        {
            return _dalRequest.GetAllRequests();
        }
    }
}
