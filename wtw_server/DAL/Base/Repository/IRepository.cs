
namespace DAL.Base.Repository
{
    // [Interface]
    public interface IRepository<T> where T : class
    {
        // [Methods]
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        void Remove(T entity);
        IQueryable<T> AsQueryable();
    }
}