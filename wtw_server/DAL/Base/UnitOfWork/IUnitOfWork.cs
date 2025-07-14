using DAL.Base.Repository;

namespace DAL.Base.UnitOfWork
{
    // [Interface]
    public interface IUnitOfWork : IDisposable
    {
        // [Methods]
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}