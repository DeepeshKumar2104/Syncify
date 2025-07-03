using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syncify.Infrastructure.Persistence.AwsDB;

namespace Syncify.Infrastructure.Repositories
{
    public class UnitOfWork
    {
        private readonly AwsDbContext awscontext;

        public UnitOfWork(AwsDbContext awscontext)
        {
            this.awscontext = awscontext;
        }

        //public async Task CommitTransactionAsync()
        //{
        //    await transaction.CommitAsync();
        //}

        //public async Task RollBackAsync()
        //{
        //    await transaction.RollbackAsync();
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await awscontext.SaveChangesAsync();
        }
        public void Dispose()
        {
            awscontext.Dispose();
        }
    }
}
