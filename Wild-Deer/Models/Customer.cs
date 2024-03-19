using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wild_Deer.Models;

public partial class Customer
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? Birthday { get; set; }

    public DateTime JoinDate { get; set; } = DateTime.Now;

    public string Adress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string? ProfileImage { get; set; }

    [Key]
    public int UserId { get; set; }

}
