namespace MiniEShop.ViewModels.Orders;

public class OrderItemViewModel
{
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}
