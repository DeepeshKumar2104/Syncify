using System;
using System.Threading.Tasks;
using Syncify.Application.Interfaces;
using Syncify.Application.Models;
using Syncify.Domain.AwsEntities;
using Syncify.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;  // You can use any serializer you prefer, like System.Text.Json

namespace Syncify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork unitofWork;
        private readonly KafkaProducer _kafkaProducer;

        public UserService(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
            _kafkaProducer = new KafkaProducer("localhost:9092", "employeeevents");  
        }

        public async Task<bool> RegisterEmployee(RequestModels models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models), "The models object cannot be null.");

            await unitofWork.BeginTransactionAsync();
            try
            {
                // Department handling
                var department = await unitofWork.DepartmentRepository
                    .FirstOrDefaultAsync(d => d.Name == models.Department.Name);
                if (department == null)
                {
                    var departmentEntity = new Department
                    {
                        Name = models.Department.Name
                    };
                    await unitofWork.DepartmentRepository.AddAsync(departmentEntity);
                    await unitofWork.SaveChangesAsync();
                    department = departmentEntity; // Ensure department is assigned
                }

                // Project handling
                var project = await unitofWork.ProjectRepository
                    .FirstOrDefaultAsync(p => p.Name == models.Project.Name);
                if (project == null)
                {
                    var projectEntity = new Project
                    {
                        Name = models.Project.Name,
                        Description = models.Project.Description
                    };
                    await unitofWork.ProjectRepository.AddAsync(projectEntity);
                    await unitofWork.SaveChangesAsync();
                    project = projectEntity; // Ensure project is assigned
                }

                // Designation handling
                var designation = await unitofWork.DesignationRepository
                    .FirstOrDefaultAsync(d => d.Title == models.Designation.Title);
                if (designation == null)
                {
                    var designationEntity = new Designation
                    {
                        Title = models.Designation.Title
                    };
                    await unitofWork.DesignationRepository.AddAsync(designationEntity);
                    await unitofWork.SaveChangesAsync();
                    designation = designationEntity; // Ensure designation is assigned
                }

                // Employee handling
                var employee = await unitofWork.EmployeeRepository
                    .FirstOrDefaultAsync(e => e.ExternalEmployeeCode == models.Employee.ExternalEmployeeCode);
                if (employee == null)
                {
                    var employeeEntity = new Employee
                    {
                        ExternalEmployeeCode = models.Employee.ExternalEmployeeCode,
                        FirstName = models.Employee.FirstName,
                        LastName = models.Employee.LastName,
                        DepartmentId = department.DepartmentId,
                        DesignationId = designation.DesignationId
                    };
                    await unitofWork.EmployeeRepository.AddAsync(employeeEntity);
                    await unitofWork.SaveChangesAsync();
                    employee = employeeEntity; // Ensure employee is assigned
                }

                // Contact handling
                var contactEntity = new Contact
                {
                    EmployeeId = employee.EmployeeId,
                    Email = models.Contact.Email,
                    Phone = models.Contact.Phone
                };
                await unitofWork.UserContactRepository.AddAsync(contactEntity);

                // Address handling
                var addressEntity = new Address
                {
                    EmployeeId = employee.EmployeeId,
                    Line1 = models.Address.Line1,
                    City = models.Address.City,
                    State = models.Address.State,
                    ZipCode = models.Address.ZipCode
                };
                await unitofWork.UserAddressRepository.AddAsync(addressEntity);

                // Check if EmployeeProject already exists
                var existingEmployeeProject = await unitofWork.EmployeeProjectRepository
                    .FirstOrDefaultAsync(ep => ep.EmployeeId == employee.EmployeeId && ep.ProjectId == project.ProjectId);

                if (existingEmployeeProject == null)
                {
                    var employeeProjectEntity = new EmployeeProject
                    {
                        EmployeeId = employee.EmployeeId,
                        ProjectId = project.ProjectId,
                        AssignedOn = models.EmployeeProject.AssignedOn
                    };
                    await unitofWork.EmployeeProjectRepository.AddAsync(employeeProjectEntity);
                }
                else
                {
                    Console.WriteLine($"Employee {employee.EmployeeId} is already assigned to project {project.ProjectId}. Skipping insertion.");
                }

                // AuditLog handling
                var auditLogEntity = new AuditLog
                {
                    EmployeeId = employee.EmployeeId,
                    Action = models.AuditLog.Action,
                    Timestamp = models.AuditLog.Timestamp
                };
                await unitofWork.AuditLogRepository.AddAsync(auditLogEntity);

                // Commit transaction
                await unitofWork.SaveChangesAsync();
                await unitofWork.CommitTransactionAsync();

                // Prepare the detailed message for Kafka
                var message = new
                {
                    Employee = new
                    {
                        employee.EmployeeId,
                        employee.ExternalEmployeeCode,
                        employee.FirstName,
                        employee.LastName,
                        Department = department.Name,
                        Designation = designation.Title
                    },
                    Contact = new
                    {
                        contactEntity.Email,
                        contactEntity.Phone
                    },
                    Address = new
                    {
                        addressEntity.Line1,
                        addressEntity.City,
                        addressEntity.State,
                        addressEntity.ZipCode
                    },
                    Project = new
                    {
                        project.Name,
                        project.Description
                    },
                    AuditLog = new
                    {
                        auditLogEntity.Action,
                        auditLogEntity.Timestamp
                    }
                };

                // Serialize message to JSON string
                var serializedMessage = JsonConvert.SerializeObject(message);

                // Send message to Kafka
                await _kafkaProducer.SendMessageAsync(serializedMessage);

                return true;
            }
            catch (Exception ex)
            {
                await unitofWork.RollBackAsync();
                throw new Exception("An error occurred while registering the employee.", ex);
            }
        }

    }
}
