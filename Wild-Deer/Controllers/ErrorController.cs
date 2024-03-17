using Microsoft.AspNetCore.Mvc;

namespace Wild_Deer.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
