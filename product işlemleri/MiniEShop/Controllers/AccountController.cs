using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniEShop.Models;
using MiniEShop.ViewModels.Account;

namespace MiniEShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if(User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index","Home");
            }
            ViewData["ReturnUrl"]=returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Eposta ya da şifre hatalı.");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            if(user is not null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index","Home",new {area="Admin"});
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index","Home");
            }
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName=model.Email,
                Email=model.Email,
                FullName=model.FullName,
                EmailConfirmed=true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, string.Join(" ", result.Errors.Select(e=>e.Description)));
                return View(model);
            }
            await _userManager.AddToRoleAsync(user,"Customer");

            await _signInManager.SignInAsync(user, isPersistent:false);
            
            return RedirectToAction("Index","Home");
        }

    }
}
