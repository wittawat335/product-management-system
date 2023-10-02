using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }
}
