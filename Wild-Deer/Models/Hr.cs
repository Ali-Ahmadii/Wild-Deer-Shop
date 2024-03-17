using System;
using System.Collections.Generic;

namespace Wild_Deer.Models;

public partial class Hr
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool EditCustomers { get; set; }

    public bool EditProducts { get; set; }

    public bool EditSellers { get; set; }

    public int Hrid { get; set; }
}
