using System;
using System.Collections.Generic;

namespace Wild_Deer.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int SellerId { get; set; }

    public string Image { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int Count { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ExtraImage? ExtraImage { get; set; }

    public virtual Seller ProductNavigation { get; set; } = null!;

    public virtual ICollection<Sold> Solds { get; set; } = new List<Sold>();
}
