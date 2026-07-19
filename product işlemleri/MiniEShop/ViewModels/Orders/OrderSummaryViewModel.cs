namespace MiniEShop.ViewModels.Orders;

public class OrderSummaryViewModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
}
