using System;
using MiniEShop.Models.Enums;

namespace MiniEShop.Models;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }=null!;
    public ApplicationUser? User { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }=OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItem> Items { get; set; }=[];
}
