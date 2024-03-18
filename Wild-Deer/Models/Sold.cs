using System;
using System.Collections.Generic;

namespace Wild_Deer.Models;

public partial class Sold
{
    public int ProductId { get; set; }

    public int SellerId { get; set; }

    public int Count { get; set; }

    public int Value { get; set; }

    public int BuyerId { get; set; }

    public DateOnly BuyingDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public bool DeliveryStatus { get; set; }

    public int SellInfoId { get; set; }

    public virtual Customer Buyer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
