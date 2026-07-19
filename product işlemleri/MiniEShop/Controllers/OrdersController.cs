using Microsoft.AspNetCore.Mvc;
using MiniEShop.Services;

namespace MiniEShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if(userId is null) return NotFound();
            var orders = await _orderService.GetMyOrdersAsync(userId);
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            if (userId is null) return NotFound();
            var order = await _orderService.GetMyOrderByIdAsync(userId,id);
            return order is null ? NotFound() : View(order);
        }



        private string? GetUserId() => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }
}
