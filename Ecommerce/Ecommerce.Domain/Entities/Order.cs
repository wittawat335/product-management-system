using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Order
{
    public Guid OrderItemId { get; set; }

    public Guid TransactionId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Transaction Transaction { get; set; }
}
