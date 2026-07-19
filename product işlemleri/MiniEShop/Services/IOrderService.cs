using System;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Orders;

namespace MiniEShop.Services;

public interface IOrderService
{
    Task<(bool Success, int? OrderId, string? Error)> CreateFromCartAsync(string userId);
    Task<IList<OrderSummaryViewModel>> GetMyOrdersAsync(string userId);
    Task<OrderDetailsViewModel?> GetMyOrderByIdAsync(string userId, int orderId);
    Task<IList<AdminOrderSummaryViewModel>> GetAllOrdersForAdminAsync();
    Task<(bool Success, string? Error)> UpdateStatusAsync(int orderId, string status);
}
