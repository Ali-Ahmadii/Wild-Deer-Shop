using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy = "SignedInCustomer")]
    public class CustomerProfileController : Controller
    {
        private readonly WildDeerContext _db;
        public CustomerProfileController(WildDeerContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            var Customer = _db.Customers.FirstOrDefault(c => c.UserId == Convert.ToInt32(nameClaim));
            if (Customer == null)
            {
                return Redirect("/SellerProfile/Index");
            }
            else
            {
                string profile = _db.Customers.FirstOrDefault(c => c.UserId == Convert.ToInt32(nameClaim)).ProfileImage;
                if (nameClaim != null)
                {
                    ViewBag.SignInStatus = true;
                    ViewBag.Name = userName;
                }
                ViewBag.Profile = profile;
                List<Sold> ss = new List<Sold>();
                ss = _db.Solds.Where(c => c.BuyerId == Convert.ToInt32(nameClaim)).ToList();
                return View(ss);
            }
        }
        public IActionResult EditAdress(string address)
        {
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            var c = _db.Customers.FirstOrDefault(c => c.UserId == Convert.ToInt32(nameClaim));
            if (nameClaim != null)
            {
                ViewBag.SignInStatus = true;
                ViewBag.Name = userName;
            }
            c.Adress = address;
            _db.SaveChanges();
            return Redirect("Index");

        }
    }

}
