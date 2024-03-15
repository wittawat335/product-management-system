using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Action
{
    public string ActId { get; set; }

    public string MenuId { get; set; }

    public string ActName { get; set; }

    public string Status { get; set; }
}
