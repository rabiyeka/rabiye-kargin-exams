using System;
using Microsoft.AspNetCore.Identity;

namespace MiniEShop.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
