using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncify.Domain.AwsEntities;



namespace Syncify.Domain.Interface
{
    public interface IUnitofWork
    {
        IGenericRepository<Address> userAddressRepository { get; }
        IGenericRepository<Contact> UserContactRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackAsync();
    }
}
