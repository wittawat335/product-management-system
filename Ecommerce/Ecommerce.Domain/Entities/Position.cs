using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Position
{
    public string PositionId { get; set; }

    public string PositionName { get; set; }

    public string MenuDefault { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Menu MenuDefaultNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
