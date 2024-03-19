using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wild_Deer.Controllers
{
    public class ShopController : Controller
    {


        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
