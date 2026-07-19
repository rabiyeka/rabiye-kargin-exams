using System;
using MiniEShop.ViewModels.Cart;

namespace MiniEShop.Services;

public interface ICartService
{
    Task<CartIndexViewModel> GetCartAsync(string userId);
    Task<(bool Success, string? Error)> AddItemAsync(string userId, int productId, int quantity);
    Task<(bool Success, string? Error)> UpdateItemAsync(string userId, int cartItemId, int quantity);
    Task<(bool Success, string? Error)> RemoveItemAsync(string userId, int cartItemId);
}
