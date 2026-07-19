namespace MiniEShop.ViewModels.Admin;

public class AdminCategoryListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int ProductCount { get; set; }
}
