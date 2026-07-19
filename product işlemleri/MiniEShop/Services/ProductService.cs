using System;
using Microsoft.EntityFrameworkCore;
using MiniEShop.Models;
using MiniEShop.Repositories;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Products;

namespace MiniEShop.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, int? ProductId, string? Error)> CreateProductAsync(AdminProductFormViewModel model)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(model.CategoryId);
        if(category is null)
        {
            return (false, null, "Geçersiz kategori");
        }
        var product = new Product
        {
            Name=model.Name.Trim(),
            Description=model.Description!.Trim(),
            Price=model.Price,
            StockQuantity=model.StockQuantity,
            ImageUrl=model.ImageUrl.Trim(),
            CategoryId=model.CategoryId
        };
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return (true, product.Id, null);
    }

    public Task CreateProductAsync(string name, string? description)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool Success, string? Error)> DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if(product is null)
        {
            return (false, "Ürün bulunamadı.");
        }
        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }

    public async Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id)
    {
        if(id<=0) return null!;
        var product = await _unitOfWork
            .Products
            .Query()
            .Include(p=>p.Category)
            .FirstOrDefaultAsync(p=>p.Id==id);
        if(product is null) return null!;
        return new ProductDetailsViewModel
        {
            Id=product.Id,
            Name=product.Name,
            Description=product.Description,
            Price=product.Price,
            StockQuantity=product.StockQuantity,
            ImageUrl=product.ImageUrl,
            CategoryName=product.Category!.Name
        };
    }

    public async Task<AdminProductFormViewModel?> GetProductForEditAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product is null)
        {
            return null;
        }
        return new AdminProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl,
             CategoryId=product.CategoryId
        };
    }

    public async Task<IList<ProductListItemViewModel>> GetProductsAsync(int? categoryId = null)
    {
        var query = _unitOfWork
            .Products
            .Query()
            .Include(p=>p.Category)
            .AsQueryable();

        if(categoryId is > 0)
        {
            query = query.Where(p=>p.CategoryId==categoryId);
        }
        return await query
            .OrderBy(p=>p.Name)
            .Select(p=>new ProductListItemViewModel
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                Price=p.Price,
                StockQuantity=p.StockQuantity,
                ImageUrl=p.ImageUrl,
                CategoryName=p.Category!.Name
            }).ToListAsync();
    }

    public Task<IList<ProductListItemViewModel>> GetProductsForAdminAsync()
    {
        return GetProductsAsync();
    }

    public async Task<(bool Success, string? Error)> UpdateProductAsync(AdminProductFormViewModel model)
    {
        var product = await _unitOfWork.Products.GetByIdAsync((int)model.Id!);
        if(product is null)
        {
            return (false, "Ürün bulunamadı.");
        }
        var category = await _unitOfWork.Categories.GetByIdAsync(model.CategoryId);
        if (category is null)
        {
            return (false, "Geçersiz kategori");
        }
        product.Name = model.Name.Trim();
        product.Description=model.Description?.Trim()!;
        product.Price=model.Price;
        product.StockQuantity=model.StockQuantity;
        product.ImageUrl=model.ImageUrl.Trim();
        product.CategoryId=model.CategoryId;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return (true,null);
    }

    public Task UpdateProductAsync(int id, string name, string? description)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool Success, string? ImageUrl, string? Error)> UploadProductImageAsync(int id, IFormFile file, IWebHostEnvironment env)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product is null)
        {
            return (false, null, "Ürün bulunamadı.");
        }
        if (file.Length == 0)
        {
            return (false, null, "Dosya hatalı!");
        }
        string[] allowed = [".jpg",".jpeg",".png",".webp",".gif"];
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowed.Contains(extension))
        {
            return (false, null, "Yalnızca resim dosyaları yüklenebilir.");
        }
        // C:\GitHub\Infotech-MSCD-ENE-9\Modul07-Backend-Development-ASP-NET-CORE-MVC-Giris\Week14\16-07-2026\MiniEShop/wwwroot/images/products
        var directory = Path.Combine(env.WebRootPath,"images","products");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var fileName = $"{id}_{Guid.NewGuid():N}{extension}";
        //C:\GitHub\Infotech-MSCD-ENE-9\Modul07-Backend-Development-ASP-NET-CORE-MVC-Giris\Week14\16-07-2026\MiniEShop/wwwroot/images/products/4_dlkgjdsf-345343453534354-435.png
        var filePath = Path.Combine(directory,fileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        product.ImageUrl = $"/images/products/{fileName}";
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return (true, product.ImageUrl, null);

    }
}
