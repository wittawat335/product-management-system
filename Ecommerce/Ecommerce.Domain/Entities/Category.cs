using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Category
{
    public string CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
