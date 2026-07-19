using System;
using Microsoft.EntityFrameworkCore;
using MiniEShop.Models;
using MiniEShop.Repositories;
using MiniEShop.ViewModels.Cart;

namespace MiniEShop.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error)> AddItemAsync(string userId, int productId, int quantity)
    {
        if(quantity<=0)
        {
            return (false, "Miktar en az 1 olmalıdır");
        }
        var product = await _unitOfWork.Products.GetByIdAsync(productId);
        if(product is null)
        {
            return (false, $"Id={productId} ürün bulunamadı.");
        }
        var cart = await GetOrCreateCartAsync(userId);
        var existingItem = cart.Items.FirstOrDefault(i=>i.ProductId==productId);
        var newQuantity = (existingItem?.Quantity ?? 0) + quantity;
        if(newQuantity > product.StockQuantity)
        {
            return (false, $"Yetersiz stook. Ürün stoğu: {product.StockQuantity}, istenen miktar: {newQuantity}");
        }
        if(existingItem is not null)
        {
            existingItem.Quantity = newQuantity;
            _unitOfWork.CartItems.Update(existingItem);
        }
        else
        {
            await _unitOfWork.CartItems.AddAsync(new CartItem
            {
                CartId=cart.Id,
                ProductId=productId,
                Quantity=quantity
            });
        }

        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }

    public async Task<CartIndexViewModel> GetCartAsync(string userId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        return new CartIndexViewModel
        {
            Items = cart.Items
                .OrderBy(i=>i.Product!.Name)
                .Select(i=>new CartItemViewModel
                {
                    Id=i.Id,
                    ProductId=i.ProductId,
                    ProductName=i.Product!.Name,
                    ImageUrl=i.Product.ImageUrl,
                    UnitPrice=i.Product.Price,
                    Quantity=i.Quantity
                }).ToList()
        };
    }
    public async Task<(bool Success, string? Error)> RemoveItemAsync(string userId, int cartItemId)
    {
        if (cartItemId <= 0)
        {
            return (false, "Geçersiz sepet satırı");
        }
        var cartItem = await GetCartItemForUserAsync(cartItemId, userId);
        if (cartItem is null)
        {
            return (false, $"Id={cartItemId} sepet satırı bulunamadı.");
        }
        _unitOfWork.CartItems.Remove(cartItem);
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateItemAsync(string userId, int cartItemId, int quantity)
    {
        if(cartItemId<=0)
        {
            return (false, "Geçersiz sepet satırı");
        }
        if(quantity<=0)
        {
            return (false, "Miktar en az 1 olmalıdır.");
        }
        var cartItem = await GetCartItemForUserAsync(cartItemId, userId);
        if(cartItem is null)
        {
            return (false, $"Id={cartItemId} sepet satırı bulunamadı.");
        }
        if (quantity > cartItem.Product!.StockQuantity)
        {
            return (false, $"Yetersiz Stok: {cartItem.Product.StockQuantity}");
        }
        cartItem.Quantity=quantity;
        _unitOfWork.CartItems.Update(cartItem);
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }

    private async Task<Cart> GetOrCreateCartAsync(string userId)
    {
        var cart = await _unitOfWork
            .Carts
            .Query()
            .Include(c=>c.Items)
            .ThenInclude(i=>i.Product)
            .FirstOrDefaultAsync(c=>c.UserId==userId);
        if(cart is not null) return cart;
        cart =  new Cart { UserId= userId};
        await _unitOfWork.Carts.AddAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        var id = cart.Id;
        cart = await _unitOfWork
            .Carts
            .Query()
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
        return cart!;
    }
    private async Task<CartItem?> GetCartItemForUserAsync(int cartItemId, string userId)
    {
        return await _unitOfWork.CartItems.Query()
            .Include(i=>i.Product)
            .Include(i=>i.Cart)
            .FirstOrDefaultAsync(i=>i.Id==cartItemId && i.Cart!.UserId==userId);
    }
}
