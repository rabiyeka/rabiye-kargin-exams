using Microsoft.AspNetCore.Mvc;
using MiniEShop.Services;

namespace MiniEShop.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly IUserAdminService _userAdminService;

        public UsersController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        public async Task<IActionResult> Index()
        {
            var redirect = RequireAdmin();
            if (redirect is not null) return redirect;
            var users = await _userAdminService.GetUsersForAdminAsync();
            return View(users);
        }

    }
}
