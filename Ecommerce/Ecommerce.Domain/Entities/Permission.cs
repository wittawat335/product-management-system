using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Permission
{
    public string PositionId { get; set; }

    public string MenuId { get; set; }

    public string ActId { get; set; }

    public string Flag { get; set; }

    public string Status { get; set; }
}
