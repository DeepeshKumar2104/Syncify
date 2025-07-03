using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class EmployeeProject
{
    public Guid EmployeeId { get; set; }

    public Guid ProjectId { get; set; }

    public DateTime? AssignedOn { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
