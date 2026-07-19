namespace MiniEShop.ViewModels.Products;

public class ProductsIndexViewModel
{
    public IList<ProductListItemViewModel> Products { get; set; } = [];
    public IList<CategoryFilterItemViewModel> Categories { get; set; } = [];
    public int? SelectedCategoryId { get; set; }
}
