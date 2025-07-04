using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Department
{
    public Guid DepartmentId { get; set; } = Guid.NewGuid();    

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
