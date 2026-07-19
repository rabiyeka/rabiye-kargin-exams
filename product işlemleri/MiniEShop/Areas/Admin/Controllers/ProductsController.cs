using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniEShop.Services;
using MiniEShop.ViewModels.Admin;


namespace MiniEShop.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        private async Task PopulateCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesForAdminAsync();
            ViewBag.Categories = categories;
        }

        public async Task<IActionResult> Index()
        {
            var redirect = RequireAdmin();
            var products = await _productService.GetProductsForAdminAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;

            await PopulateCategoriesAsync();
            return View(new AdminProductFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminProductFormViewModel model)
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync();
                return View(model);
            }
            await _productService.CreateProductAsync(model);
            TempData["Success"] = "Ürün eklendi";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            var model = await _productService.GetProductForEditAsync(id);
            if (model is null) return NotFound();

            await PopulateCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminProductFormViewModel model)
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            model.Id = id;
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync();
                return View(model);
            }

            var (success, error) = await _productService.UpdateProductAsync(model);
            if (!success)
            {
                await PopulateCategoriesAsync();
                ModelState.AddModelError(string.Empty, error ?? "Ürün güncellenemedi.");
                return View(model);
            }

            TempData["Success"] = "Ürün güncellendi";
            return RedirectToAction(nameof(Index));
        }
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            var (success, error) = await _productService.DeleteProductAsync(id);
            TempData[success ? "Success" : "Error"] = success ? "Ürün silindi." : error;
            return RedirectToAction(nameof(Index));
        }
    }
}