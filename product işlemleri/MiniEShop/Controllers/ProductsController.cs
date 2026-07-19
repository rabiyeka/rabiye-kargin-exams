using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEShop.Services;
using MiniEShop.ViewModels.Products;

namespace MiniEShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ICartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var model = new ProductsIndexViewModel
            {
                Products = await _productService.GetProductsAsync(categoryId),
                Categories = await _categoryService.GetCategoriesForFilterAsync(),
                SelectedCategoryId=categoryId
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id<=0) return NotFound();
            var product = await _productService.GetProductDetailsAsync(id);
            return product is null? NotFound() : View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1, string? returnUrl = null)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;//O sırada login olmuş olan kullanıcının Id'si.
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var (success, error) = await _cartService.AddItemAsync(userId, productId, quantity);
            if (!success)
            {
                TempData["Error"] = error;
                return Redirect(returnUrl ?? Url.Action("Details",new {id=productId})!);
            }
            TempData["Success"]="Ürün sepete eklendi.";
            return Redirect(returnUrl ?? Url.Action("Index","Cart")!);
        }

    }
}
