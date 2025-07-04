using System;
using System.Collections.Generic;

namespace Syncify.Domain.AwsEntities;

public partial class Address
{
    public Guid AddressId { get; set; }= Guid.NewGuid();

    public Guid EmployeeId { get; set; }

    public string? Line1 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
