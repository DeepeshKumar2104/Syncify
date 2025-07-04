using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class AuditLog
{
    public Guid LogId { get; set; } = Guid.NewGuid();

    public Guid EmployeeId { get; set; }

    public string? Action { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
