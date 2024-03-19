using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    public class CacheController : Controller
    {
        static int flag = 0;
        private readonly WildDeerContext _db;
        private readonly IDistributedCache redis;
        public CacheController(WildDeerContext db,IDistributedCache redis)
        {
            _db = db;
            this.redis = redis;
        }
        [Authorize (Policy = "SignedInCustomer")]
        public IActionResult CachePlease(int ProductID)
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string referrerUrl = HttpContext.Request.Headers["Referer"].ToString();
            Product TargerProduct = _db.Products.FirstOrDefault(p => p.ProductId == ProductID);
            string key = "AddedProductsByID:" + nameClaim;
            if(flag == 0)
            {
                List<Product> added_to_cart_products = new List<Product>();
                added_to_cart_products.Add(TargerProduct);
                redis.SetString(key, JsonSerializer.Serialize(added_to_cart_products), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(5)
                });




            }
            else
            {
                //get list from redis
                List<Product> PrevList = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
                //add new member to list
                PrevList.Add(TargerProduct);
                //set to redis
                redis.SetString(key, JsonSerializer.Serialize(PrevList), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(5)
                });
            }

            List<Product> x = JsonSerializer.Deserialize<List<Product>>(redis.GetString(key));
            flag++;
            //return Redirect("/Detail/Detail/"+"?id="+ProductID);
            return Redirect(referrerUrl);
        }

    }
}
