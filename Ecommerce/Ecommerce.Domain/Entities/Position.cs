using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Position
{
    public Guid PositionId { get; set; }

    public string PositionName { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<MenuPosition> MenuPositions { get; set; } = new List<MenuPosition>();

    public virtual ICollection<UserPosition> UserPositions { get; set; } = new List<UserPosition>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
