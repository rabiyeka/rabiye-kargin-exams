using Microsoft.AspNetCore.Mvc;

namespace MiniEShop.Areas.Admin.Controllers
{
    
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            var redirect = RequireAdmin();
            return redirect ?? View();
        }

    }
}
