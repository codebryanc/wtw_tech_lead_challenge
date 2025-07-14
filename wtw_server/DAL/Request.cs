using Entity;

namespace DAL
{
    public interface IRequest
    {
        List<Requests> GetAllRequests();
    }

    public class Request : IRequest
    {
        public List<Requests> GetAllRequests()
        {
            return new List<Requests>
            {
                new Requests
                {
                    reqId = Guid.Parse("F2348456-A1E0-4EF1-B777-4651B392D14D"),
                    rtyId = Guid.Parse("F2348456-A1E0-4EF1-B777-4651B392D14D"),
                    resId = Guid.Parse("9B484C2A-3D94-4C40-BC12-E61F3A8B0302"),
                    createdAt = DateTime.Parse("2025-07-14 12:25:46.6333333"),
                    data = "{\"date\":\"2025-07-05\",\"hours\":1,\"reason\":\"Early leave\"}"
                }
            };
        }
    }
}
