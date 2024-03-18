using System;
using System.Collections.Generic;

namespace Wild_Deer.Models;

public partial class Seller
{
    public string OwnerName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime JoinedDate { get; set; } = DateTime.Now;

    public string? MostProductsEra { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public int SellerId { get; set; }

}
