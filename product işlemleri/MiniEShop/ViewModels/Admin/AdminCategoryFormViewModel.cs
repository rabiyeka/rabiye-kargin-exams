using System.ComponentModel.DataAnnotations;

namespace MiniEShop.ViewModels.Admin;

public class AdminCategoryFormViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [MaxLength(100)]
    [Display(Name = "Kategori Adı")]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }
}
