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
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<Project> ProjectRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<Address> UserAddressRepository { get; }
        IGenericRepository<Contact> UserContactRepository { get; }
        IGenericRepository<AuditLog> AuditLogRepository { get; }
        IGenericRepository<Designation> DesignationRepository { get; }
        IGenericRepository<EmployeeProject> EmployeeProjectRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackAsync();
    }
}
