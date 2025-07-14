using DAL.Base.Data;
using DAL.Base.Repository;

namespace DAL.Base.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // [Properties]
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        // [Constructor]
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        // [Methods]
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<T>(_context);
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // [Dispose]
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}