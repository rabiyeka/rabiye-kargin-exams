namespace MiniEShop.ViewModels.Products;

public class ProductDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}
