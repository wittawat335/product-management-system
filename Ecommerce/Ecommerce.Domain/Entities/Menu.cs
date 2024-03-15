using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Menu
{
    public string MenuId { get; set; }

    public string MenuName { get; set; }

    public int? MenuLevel { get; set; }

    public string ParentId { get; set; }

    public string Url { get; set; }

    public int MenuOrder { get; set; }

    public string Icon { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
