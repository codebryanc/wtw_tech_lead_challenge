using Microsoft.EntityFrameworkCore;

namespace DAL.Base.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // [Properties]
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        // [Constructor]
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // [Methods]
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}