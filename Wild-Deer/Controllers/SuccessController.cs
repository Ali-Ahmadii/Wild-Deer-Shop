using Microsoft.AspNetCore.Mvc;

namespace Wild_Deer.Controllers
{
    public class SuccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
