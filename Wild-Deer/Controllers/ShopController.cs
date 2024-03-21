using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy = "SignedInCustomer")]
    public class ShopController : Controller
    {

        private readonly IDistributedCache redis;
        public ShopController(IDistributedCache chache)
        {
            redis = chache;
        }



        public IActionResult Index()
        {
            //if redis is empty shop
            //else
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string key = "AddedProductsByID:" + nameClaim;
            var eq = redis.GetString(key);
            List<Product> products = new List<Product>();
            if (eq != null)
            {
                products = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
                List<Product> send_these = new List<Product>();


                for (int i = 0; i < products.Count; i++)
                {
                    if (ViewData["ProductID" + products[i].ProductId] == null)
                    {
                        ViewData["ProductID" + products[i].ProductId] = products.Where(vvvvv => vvvvv.ProductId == products[i].ProductId).ToList().Count;
                        send_these.Add(products[i]);
                    }
                }

                return View(send_these);
            }
            else
            {
                return View(null);
            }

        }
        


        public IActionResult Plus(int productID)
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string key = "AddedProductsByID:" + nameClaim;
            List<Product> products = new List<Product>();
            products = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
            Product target = products.FirstOrDefault(x=>x.ProductId == productID);
            products.Add(target);
            redis.SetString(key, JsonSerializer.Serialize(products), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(5)
            });
            return Redirect("Index");
        }
        public IActionResult Minus(int productID)
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string key = "AddedProductsByID:" + nameClaim;
            List<Product> products = new List<Product>();
            products = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
            Product target = products.FirstOrDefault(x => x.ProductId == productID);
            products.Remove(target);
            redis.SetString(key, JsonSerializer.Serialize(products), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(5)
            });
            return Redirect("Index");
        }
    }
}
