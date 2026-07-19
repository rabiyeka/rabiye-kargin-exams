using System.ComponentModel.DataAnnotations;

namespace MiniEShop.ViewModels.Admin;

public class AdminProductFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(150)]
    [Display(Name = "Ürün Adı")]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    [Display(Name = "Fiyat")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Stok")]
    public int StockQuantity { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name = "Görsel URL")]
    public string ImageUrl { get; set; } = "https://placehold.co/400x400?text=Yeni+Urun";

    [Range(1, int.MaxValue)]
    [Display(Name = "Kategori")]
    public int CategoryId { get; set; }
}
