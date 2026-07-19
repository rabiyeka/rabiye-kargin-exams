using System.ComponentModel.DataAnnotations;

namespace MiniEShop.ViewModels.Account;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password),ErrorMessage ="Şifreler eşleşmiyor.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    public string ConfirmPassword { get; set; } = null!;
}
