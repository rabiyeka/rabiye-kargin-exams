using System;
using MiniEShop.Models;

namespace MiniEShop.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<Category> Categories { get; }
    IRepository<Product> Products { get; }
    IRepository<Cart> Carts { get; }
    IRepository<CartItem> CartItems { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync();
    Task ExecuteTransactionAsync(Func<Task> action);

}
