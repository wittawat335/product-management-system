using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Discount { get; set; }

    public decimal ShippingCost { get; set; }

    public decimal TaxPercent { get; set; }

    public decimal Total { get; set; }

    public decimal Paid { get; set; }

    public decimal Change { get; set; }

    public string OrderList { get; set; }

    public string PaymentType { get; set; }

    public string PaymentDetail { get; set; }

    public string EmployeeId { get; set; }

    public string SellerId { get; set; }

    public string BuyerId { get; set; }

    public string Comment { get; set; }

    public DateTime? Timestamp { get; set; }

    public string Status { get; set; }
}
