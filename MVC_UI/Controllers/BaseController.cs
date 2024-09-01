using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MVC_UI.Controllers
{
    public class BaseController : Controller
    {
        protected INotyfService notyfService => HttpContext.RequestServices.GetService<INotyfService>();
        protected void SuccesNotyf(string message)
        {
            notyfService.Success(message);  
        }
        
        protected void ErrorNotyf(string message)
        {
            notyfService.Error(message);
        }
        
    }
}
