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
        public IGenericRepository<Department> DepartmentRepository { get; }
        public IGenericRepository<Project> ProjectRepository { get; }
        public IGenericRepository<Employee> EmployeeRepository { get; }
        public IGenericRepository<Address> UserAddressRepository { get; }
        public IGenericRepository<Contact> UserContactRepository { get; }
        public IGenericRepository<AuditLog> AuditLogRepository { get; }
        public IGenericRepository<Designation> DesignationRepository { get; }
        public IGenericRepository<EmployeeProject> EmployeeProjectRepository { get; }

        public UnitOfWork(AwsDbContext awscontext)
        {
            this.awscontext = awscontext;
            DepartmentRepository = new GenericRepository<Department>(awscontext);
            ProjectRepository = new GenericRepository<Project>(awscontext);
            EmployeeRepository = new GenericRepository<Employee>(awscontext);
            UserAddressRepository = new GenericRepository<Address>(awscontext);
            UserContactRepository = new GenericRepository<Contact>(awscontext);
            AuditLogRepository = new GenericRepository<AuditLog>(awscontext); 
            DesignationRepository = new GenericRepository<Designation>(awscontext);
            EmployeeProjectRepository = new GenericRepository<EmployeeProject>(awscontext);
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
