namespace MiniEShop.ViewModels.Orders;

public class OrderDetailsViewModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public IList<OrderItemViewModel> Items { get; set; } = [];
}
