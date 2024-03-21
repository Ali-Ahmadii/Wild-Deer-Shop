using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy = "SignedInCustomer")]
    public class PaymentController : Controller
    {
        private readonly WildDeerContext _context;
        private readonly IDistributedCache redis;
        public PaymentController(WildDeerContext context,IDistributedCache redis)
        {
            _context = context;
            this.redis = redis;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult success() 
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string key = "AddedProductsByID:" + nameClaim;
            List<Product> products = new List<Product>();
            products = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
            List<int> added = new List<int>();
            List<Sold> sold = new List<Sold>();
            for (int i = 0; i < products.Count; i++)
            {
                if (!added.Contains(products[i].ProductId))
                {
                    Sold forsale = new Sold();
                    forsale.ProductId = products[i].ProductId;
                    int count = products.Where(vvvvv => vvvvv.ProductId == products[i].ProductId).ToList().Count;
                    forsale.Value = count * products[i].Price;
                    forsale.BuyerId = Convert.ToInt32(nameClaim);
                    forsale.Count = count;
                    forsale.SellerId = products[i].SellerId;
                    added.Add(products[i].ProductId);
                    sold.Add(forsale);
                }
            }
            foreach (Sold s in sold)
            {
                _context.Add(s);
            }


            _context.SaveChanges();

            redis.Remove(key);

            //save in sold db
            return Redirect("result/?result="+"success");
        }
        public IActionResult failed() 
        {
            ViewBag.report = "failed";
             return Redirect("result/?result=" + "failed");
        }
        public IActionResult result(string result)
        {
            ViewData["report"] = result;
            return View();
        }
        public IActionResult TOHome()
        {
            return Redirect("/Home/Index");
        }
    }
}
