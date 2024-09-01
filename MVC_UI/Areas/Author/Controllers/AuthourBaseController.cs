using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_UI.Areas.Author.Controllers
{
    [Area("Authour")]
    [Authorize(Roles ="Author")]
    public class AuthourBaseController : Controller
    {
       
    }
}
