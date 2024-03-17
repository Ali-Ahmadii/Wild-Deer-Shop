using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    public class CacheController : Controller
    {
        public static List<Product> pp = new List<Product>();
        private readonly WildDeerContext _db;
        public CacheController(WildDeerContext db)
        {
            _db = db;
        }
        public IActionResult CachePlease(int ProductID)
        {
            Product TargerProduct = _db.Products.FirstOrDefault(p => p.ProductId == ProductID);
            pp.Add(TargerProduct);

            return Redirect("/Detail/Detail/"+"?id="+ProductID);
        }

    }
}
