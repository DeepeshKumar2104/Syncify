using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Contact
{
    public Guid ContactId { get; set; } = Guid.NewGuid();

    public Guid EmployeeId { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
