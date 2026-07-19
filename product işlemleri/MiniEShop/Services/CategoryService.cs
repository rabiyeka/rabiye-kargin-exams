using System;
using Microsoft.EntityFrameworkCore;
using MiniEShop.Models;
using MiniEShop.Repositories;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Products;

namespace MiniEShop.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error)> CreateCategoryAsync(string name, string? description)
    {
        var category = await _unitOfWork
            .Categories
            .Query()
            .Where(c=>c.Name.ToLower()==c.Name)
            .FirstOrDefaultAsync();
        if(category is not null)
        {
            return (false, "Bu isimde bir kategori zaten var.");
        }
        await _unitOfWork.Categories.AddAsync(new Category
        {
            Name=name.Trim(),
            Description=description?.Trim()!
        });
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork
            .Categories
            .Query()
            .Include(c=>c.Products)
            .FirstOrDefaultAsync(c=>c.Id==id);
        if (category is null) return (false, "Kategori bulunamadı.");
        if (category.Products.Any())
        {
            return (false, "Bu kategoride ürün var. Önce ürünleri silin.");
        }
        _unitOfWork.Categories.Remove(category);
        await _unitOfWork.SaveChangesAsync();
        return(true, null);
    }

    public async Task<IList<AdminCategoryListItemViewModel>> GetCategoriesForAdminAsync()
    {
        return await _unitOfWork.Categories.Query()
                    .Select(c => new AdminCategoryListItemViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description=c.Description,
                        ProductCount=c.Products.Count
                    })
                    .OrderBy(c => c.Name)
                    .ToListAsync();
    }

    public async Task<IList<CategoryFilterItemViewModel>> GetCategoriesForFilterAsync()
    {
        return await _unitOfWork.Categories.Query()
            .Select(c=>new CategoryFilterItemViewModel
            {
                Id=c.Id,
                Name=c.Name,
                ProductCount=c.Products.Count
            })
            .OrderBy(c=>c.Name)
            .ToListAsync();
    }

    public async Task<AdminCategoryFormViewModel?> GetCategoryForEditAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if(category is null) return null;
        return new AdminCategoryFormViewModel
        {
            Id=category.Id,
            Name=category.Name,
            Description=category.Description
        };
    }

    public async Task<(bool Success, string? Error)> UpdateCategoryAsync(int id, string name, string? description)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null) return (false, "Kategori bulunamadı.");
        category.Name=name;
        category.Description=description?.Trim()!;
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }
}
