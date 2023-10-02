using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class MenuPosition
{
    public Guid MenuPositionId { get; set; }

    public Guid MenuId { get; set; }

    public Guid PositionId { get; set; }

    public string Status { get; set; }

    public virtual Menu Menu { get; set; }

    public virtual Position Position { get; set; }
}
