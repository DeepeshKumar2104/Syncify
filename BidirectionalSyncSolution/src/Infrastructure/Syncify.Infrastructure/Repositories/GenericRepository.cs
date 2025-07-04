using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syncify.Domain.Interface;
using Syncify.Infrastructure.Persistence.AwsDB;

namespace Syncify.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AwsDbContext awsContext;
        public readonly DbSet<T> dbSet;

        public GenericRepository(AwsDbContext awsContext)
        {
            this.awsContext = awsContext;
            dbSet = awsContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<T?> FindByIdAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await Task.CompletedTask;
        }

        // Implementing FirstOrDefaultAsync
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
