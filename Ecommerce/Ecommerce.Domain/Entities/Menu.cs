using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Menu
{
    public Guid MenuId { get; set; }

    public string MenuName { get; set; }

    public int? MenuLevel { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; }

    public int? MenuOrder { get; set; }

    public string Icon { get; set; }

    public string Status { get; set; }

    public virtual ICollection<MenuPosition> MenuPositions { get; set; } = new List<MenuPosition>();
}
