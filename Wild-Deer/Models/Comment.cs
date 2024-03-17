using System;
using System.Collections.Generic;

namespace Wild_Deer.Models;

public partial class Comment
{
    public string Username { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ProductId { get; set; }

    public double Score { get; set; }

    public int CommentId { get; set; }

    public int? WriterId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer? Writer { get; set; }
}
