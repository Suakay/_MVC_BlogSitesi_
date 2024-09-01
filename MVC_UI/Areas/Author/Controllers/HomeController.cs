using Microsoft.AspNetCore.Mvc;

namespace MVC_UI.Areas.Author.Controllers
{
    public class HomeController : AuthourBaseController
    {
        public async Task< IActionResult> Index()
        {
            return View();
        }
    }
}
