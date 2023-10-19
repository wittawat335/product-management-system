using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Product
{
    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public string CategoryId { get; set; }

    public string Image { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Category Category { get; set; }
}
