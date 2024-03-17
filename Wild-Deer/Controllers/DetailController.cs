using Wild_Deer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Wild_Deer.Controllers
{
    public class DetailController : Controller
    {
        public int ProductID { get; set; }
        private readonly WildDeerContext _contaxt;
        public DetailController(WildDeerContext dbcontext)
        {
            this._contaxt = dbcontext;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Detail(int id)
        {
            Product TargerProduct = _contaxt.Products.FirstOrDefault(p => p.ProductId == id);
           List<Comment> tt = _contaxt.Comments.Where(x=>x.ProductId == id).ToList();
            Seller seller = _contaxt.Sellers.FirstOrDefault(p=>p.SellerId == TargerProduct.SellerId);
            ExtraImage extra = _contaxt.ExtraImages.FirstOrDefault(p => p.ProductId == id);
            ViewData["ExtraImages"] = extra;
            ViewData["Seller"] = seller.CompanyName;
            ViewData["Comments"] = tt;
            ViewData["Contact"] = seller.Email;
            return View(TargerProduct);
        }
    }
}
