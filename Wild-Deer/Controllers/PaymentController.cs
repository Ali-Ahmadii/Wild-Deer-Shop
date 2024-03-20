using Microsoft.AspNetCore.Mvc;

namespace Wild_Deer.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DoPayment() 
        {
            //save in sold db

            return null;
        }
        public IActionResult Cancell() 
        {
            return null;
        }
    }
}
