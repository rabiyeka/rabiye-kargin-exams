using System;

namespace MiniEShop.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser? User { get; set; }
    public ICollection<CartItem> Items { get; set; } = [];
}
