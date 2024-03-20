using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Wild_Deer.Controllers
{
    public class ShopController : Controller
    {
        private readonly IDistributedCache redis;
        public ShopController(IDistributedCache chache)
        {
            redis = chache;
        }
        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult Index()
        {
            //if redis is empty shop
            //else
            var id = "id";
            ViewData["ProductID" + id] = "count how may thar object in list?";
            return View();
        }
        public IActionResult DeleteItem()
        {
            //delete from redis and redirect to index
            return null;
        }
        public IActionResult Plus()
        {
            return null;
        }
        public IActionResult Minus()
        {
            return null;
        }
        public IActionResult ToPayment()
        {
            //new redis that stores list of count and price of each item with help of prev redis
            //calculate and send them to pay
            return null;//information to payment section //remember to remove items if cancelled or was success
        }
    }
}
