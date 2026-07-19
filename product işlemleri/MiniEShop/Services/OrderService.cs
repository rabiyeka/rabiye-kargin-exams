using System;
using Microsoft.EntityFrameworkCore;
using MiniEShop.Models;
using MiniEShop.Models.Enums;
using MiniEShop.Repositories;
using MiniEShop.ViewModels.Admin;
using MiniEShop.ViewModels.Orders;

namespace MiniEShop.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, int? OrderId, string? Error)> CreateFromCartAsync(string userId)
    {
        var cart = await _unitOfWork
            .Carts
            .Query()
            .Include(c=>c.Items)
            .ThenInclude(i=>i.Product)
            .FirstOrDefaultAsync(c=>c.UserId==userId);
        
        if(cart is null || cart.Items.Count == 0)
        {
            return (false, null, "Sepetiniz boş. Sipariş oluşturulamadı.");
        }

        foreach (var cartItem in cart.Items)
        {
            var product = cartItem.Product;
            if(product is null)
            {
                return (false, null, $"Id:{cartItem.ProductId} id'li ürün bulunamadı.");
            }
            if (cartItem.Quantity > product.StockQuantity)
            {
                return (false, null, $"Yetersiz stok: {product.Name}, Stok: {product.StockQuantity}, istenen: {cartItem.Quantity}");
            }
        }

        int? createdOrderId = null;
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cart.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(cartItem.ProductId);
                if(product is null)
                {
                    throw new InvalidOperationException($"Id={cartItem.ProductId} id'li ürün bulunamadı.");
                }
                product.StockQuantity -= cartItem.Quantity;
                _unitOfWork.Products.Update(product);
                totalAmount += product.Price * cartItem.Quantity;
                orderItems.Add(new OrderItem
                {
                    ProductId=product.Id,
                    Quantity=cartItem.Quantity,
                    UnitPrice=product.Price
                });
            }
            var order = new Order
            {
                UserId= userId,
                OrderDate = DateTime.UtcNow,
                Status=OrderStatus.Pending,
                TotalAmount=totalAmount,
                Items=orderItems
            };
            await _unitOfWork.Orders.AddAsync(order);

            foreach (var item in cart.Items.ToList())
            {
                _unitOfWork.CartItems.Remove(item);
            }

            await _unitOfWork.SaveChangesAsync();
            createdOrderId = order.Id;
        });


        return (true, createdOrderId, null);
    }

    public async Task<IList<AdminOrderSummaryViewModel>> GetAllOrdersForAdminAsync()
    {
        return await _unitOfWork
            .Orders
            .Query()
            .Include(o=>o.User)
            .OrderByDescending(o=>o.OrderDate)
            .Select(o=>new AdminOrderSummaryViewModel
            {
                Id=o.Id,
                OrderDate=o.OrderDate,
                Status=o.Status.ToString(),
                TotalAmount=o.TotalAmount,
                ItemCount=o.Items.Sum(i=>i.Quantity),
                CustomerEmail = o.User!.Email ?? string.Empty
            }).ToListAsync();
    }

    public async Task<OrderDetailsViewModel?> GetMyOrderByIdAsync(string userId, int orderId)
    {
        if(orderId<=0) return null;
        var order = await _unitOfWork
            .Orders
            .Query()
            .Include(o=>o.Items)
            .ThenInclude(i=>i.Product)
            .FirstOrDefaultAsync(o=>o.Id==orderId && o.UserId==userId);
        if(order is null) return null;
        return new OrderDetailsViewModel
            {
                Id=order.Id,
                OrderDate=order.OrderDate,
                Status=order.Status.ToString(),
                TotalAmount=order.TotalAmount,
                Items =order.Items.Select(i=>new OrderItemViewModel
                {
                    ProductName=i.Product!.Name,
                    Quantity=i.Quantity,
                    UnitPrice=i.UnitPrice
                }).ToList()
            };
    }

    public async Task<IList<OrderSummaryViewModel>> GetMyOrdersAsync(string userId)
    {
        return await _unitOfWork
                .Orders
                .Query()
                .Where(o=>o.UserId==userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    ItemCount = o.Items.Sum(i => i.Quantity)
                }).ToListAsync();
    }

    public async Task<(bool Success, string? Error)> UpdateStatusAsync(int orderId, string status)
    {
        if (orderId <= 0) return (false, "Geçersiz sipariş kimliği");
        if(!Enum.TryParse<OrderStatus>(status, ignoreCase: true, out var newStatus))
        {
            return (false, "Geçersiz durum. Pending ya da Shipping olmalıdır.");
        }
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        if(order is null) return (false, "Sipariş bulunamadı.");
        order.Status=newStatus;
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync();
        return (true, null);
    }
}
