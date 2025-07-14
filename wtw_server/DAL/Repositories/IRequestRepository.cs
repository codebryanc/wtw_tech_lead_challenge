using Entity;
namespace DAL.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Requests>> GetAllAsync();
    }
}