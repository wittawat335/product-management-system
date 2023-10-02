using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class UserPosition
{
    public Guid UserId { get; set; }

    public Guid PositionId { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Position Position { get; set; }

    public virtual User User { get; set; }
}
