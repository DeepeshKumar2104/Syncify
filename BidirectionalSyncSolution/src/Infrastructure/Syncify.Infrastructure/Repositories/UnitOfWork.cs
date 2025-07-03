using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Syncify.Domain.AwsEntities;
using Syncify.Domain.Interface;
using Syncify.Infrastructure.Persistence.AwsDB;

namespace Syncify.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitofWork
    {
        private readonly AwsDbContext awscontext;
        public IGenericRepository<Address> userAddressRepository { get; }

        public IGenericRepository<Contact> UserContactRepository { get; }
        public UnitOfWork(AwsDbContext awscontext)
        {
            this.awscontext = awscontext;
            userAddressRepository = new GenericRepository<Address>(awscontext);
            UserContactRepository = new GenericRepository<Contact>(awscontext);  
        }

        

        private IDbContextTransaction transaction;
        public async Task BeginTransactionAsync()
        {
            transaction =   awscontext.Database.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await transaction.CommitAsync();    
        }

        public async Task RollBackAsync()
        {
            await transaction.RollbackAsync();  
        }

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
