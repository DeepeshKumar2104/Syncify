using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public string ExternalEmployeeCode { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? DesignationId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual Department? Department { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}
