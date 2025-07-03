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
        IGenricRepository<Address> UserDetailRepository { get; }
        IGenricRepository<Contact> UserCredentialRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackAsync();
    }
}
