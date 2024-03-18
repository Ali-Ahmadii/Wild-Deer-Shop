using Microsoft.AspNetCore.Mvc;
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
            string referrerUrl = HttpContext.Request.Headers["Referer"].ToString();
            Product TargerProduct = _db.Products.FirstOrDefault(p => p.ProductId == ProductID);
            pp.Add(TargerProduct);
            //return Redirect("/Detail/Detail/"+"?id="+ProductID);
            return Redirect(referrerUrl);
        }

    }
}
