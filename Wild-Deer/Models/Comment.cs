using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wild_Deer.Models;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }
    public string Username { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ProductId { get; set; }

    public double Score { get; set; }


    public int? WriterId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer? Writer { get; set; }
}
