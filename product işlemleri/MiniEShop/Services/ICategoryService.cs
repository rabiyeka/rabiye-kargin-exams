using System;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Products;

namespace MiniEShop.Services;

public interface ICategoryService
{
    Task<IList<CategoryFilterItemViewModel>> GetCategoriesForFilterAsync();
    Task<IList<AdminCategoryListItemViewModel>> GetCategoriesForAdminAsync();
    Task<AdminCategoryFormViewModel?> GetCategoryForEditAsync(int id);
    Task<(bool Success, string? Error)> CreateCategoryAsync(string name, string? description);
    Task<(bool Success, string? Error)> UpdateCategoryAsync(int id, string name, string? description);
    Task<(bool Success, string? Error)> DeleteCategoryAsync(int id);
}
