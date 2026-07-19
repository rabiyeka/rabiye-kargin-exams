using Microsoft.AspNetCore.Mvc;
using MiniEShop.Services;

namespace MiniEShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if(userId is null) return NotFound();
            var model = await _cartService.GetCartAsync(userId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int cartItemId, int quantity)
        {
            var userId = GetUserId();
            if (userId is null) return NotFound();
            var (success, error) = await _cartService.UpdateItemAsync(userId,cartItemId,quantity);
            TempData[success ? "Success" : "Error"]= success? "Sepet güncellendi." : error;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userId = GetUserId();
            if (userId is null) return NotFound();
            var (success, error) = await _cartService.RemoveItemAsync(userId, cartItemId);
            TempData[success ? "Success" : "Error"] = success ? "Ürün sepetten çıkarıldı." : error;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();
            if (userId is null) return NotFound();
            var (success, orderId, error) = await _orderService.CreateFromCartAsync(userId);
            if(!success || orderId is null)
            {
                TempData["Error"]=error;
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Siparişiniz başarıyla oluşturuldu.";
            return RedirectToAction("Details","Orders",new {id=orderId});
        }

        private string? GetUserId() => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }
}
