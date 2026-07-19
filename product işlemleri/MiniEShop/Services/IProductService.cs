using System;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Products;

namespace MiniEShop.Services;

public interface IProductService
{
    Task<IList<ProductListItemViewModel>> GetProductsAsync(int? categoryId=null);
    Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id);
    Task<IList<ProductListItemViewModel>> GetProductsForAdminAsync();
    Task<AdminProductFormViewModel?> GetProductForEditAsync(int id);
    Task<(bool Success, int? ProductId, string? Error)> CreateProductAsync(AdminProductFormViewModel model);
    Task<(bool Success, string? Error)> UpdateProductAsync(AdminProductFormViewModel model);
    Task<(bool Success, string? Error)> DeleteProductAsync(int id);
    Task<(bool Success, string? ImageUrl, string? Error)> UploadProductImageAsync(int id, IFormFile file, IWebHostEnvironment env);
    Task CreateProductAsync(string name, string? description);
    Task UpdateProductAsync(int id, string name, string? description);
}
