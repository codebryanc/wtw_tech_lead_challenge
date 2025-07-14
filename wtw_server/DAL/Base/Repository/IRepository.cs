using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Base.Repository
{
    // [Interface]
    public interface IRepository<T> where T : class
    {
        // [Methods]
        Task<IEnumerable<T>> GetAllAsync();
    }
}