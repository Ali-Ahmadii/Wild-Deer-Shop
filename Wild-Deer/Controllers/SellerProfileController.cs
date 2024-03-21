using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy = "SignedInSeller")]
    public class SellerProfileController : Controller
    {
        
        private readonly WildDeerContext _db;
        public SellerProfileController(WildDeerContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "SellerID")?.Value;
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            if (nameClaim != null)
            {
                ViewBag.SignInStatus = true;
                ViewBag.Name = userName;
            }
            required_model Model_To_Send = new required_model();
            List<Product>products_of_seller = new List<Product>();
            products_of_seller = _db.Products.Where(c => c.SellerId == Convert.ToInt32(nameClaim)).ToList();
            List<Sold> Sale_of_seller = _db.Solds.Where(c=>c.SellerId == Convert.ToInt32(nameClaim)).ToList();



            //no product problem

            Model_To_Send.products_of_seller = products_of_seller;
            Model_To_Send.sold_products = Sale_of_seller;
            return View(Model_To_Send);
        }
        [HttpPost]
        public IActionResult Add_Product(string title,string description, string price,string count,string category,string image_title,string exi1, string exi2, string exi3, string exi4, string exi5, string exi6)
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "SellerID")?.Value;
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            Product product = new Product();
            product.Title = title;
            product.Description = description;
            product.SellerId = Convert.ToInt32(nameClaim);
            product.Image = image_title;
            product.Price = Convert.ToInt32(price);
            product.Count = Convert.ToInt32(count);
            product.Category = category;
            _db.Products.Add(product);
            _db.SaveChanges();
            int product_id = product.ProductId;
            ExtraImage ex = new ExtraImage();
            ex.ProductId = product_id;
            ex.Img1 = exi1;
            ex.Img2 = exi2;
            ex.Img3 = exi3;
            ex.Img4 = exi4;
            ex.Img5 = exi5;
            ex.Img6 = exi6;
            _db.ExtraImages.Add(ex);
            _db.SaveChanges();
            return Redirect("Index");
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
