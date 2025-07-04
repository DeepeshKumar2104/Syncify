using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Syncify.Domain.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FindByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate); // New method
    }
}
