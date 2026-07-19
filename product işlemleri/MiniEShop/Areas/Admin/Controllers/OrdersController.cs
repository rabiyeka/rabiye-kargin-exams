using Microsoft.AspNetCore.Mvc;
using MiniEShop.Services;

namespace MiniEShop.Areas.Admin.Controllers
{
    public class OrdersController : AdminBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            var orders = await _orderService.GetAllOrdersForAdminAsync();
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            var (success, error) = await _orderService.UpdateStatusAsync(id, status);
            TempData[success ? "Success" : "Error"]=success? $"Sipariş #{id} -> {status}" : error;
            return RedirectToAction(nameof(Index));
        }

    }
}
