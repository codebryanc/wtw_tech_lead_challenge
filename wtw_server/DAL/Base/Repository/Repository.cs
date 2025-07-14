using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}