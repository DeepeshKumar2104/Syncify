using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Designation
{
    public Guid DesignationId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
