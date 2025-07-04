using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Project
{
    public Guid ProjectId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}
