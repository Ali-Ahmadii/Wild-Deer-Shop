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

    public DateTime BuyingDate { get; set; } = DateTime.Now;



    public int SellInfoId { get; set; }

}
