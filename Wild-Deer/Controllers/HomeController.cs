using Wild_Deer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Wild_Deer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WildDeerContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, WildDeerContext context)
        {
            _logger = logger;
            this._dbcontext = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> db_products = _dbcontext.Products.ToList();

            //var auth = User.FindFirst(ClaimTypes.Role);
            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Anonymous") };
            //var identity = new ClaimsIdentity(claims,"PublicCookie");
            //ClaimsPrincipal claimprincipal = new ClaimsPrincipal(identity);
            //ViewBag.SignInStatus = true;
            //ViewBag.Name = "Ali";
            ViewData["IMG1"] = "/img/12.jpg";
            ViewData["IMG2"] = "/img/back.jpg";
            ViewData["IMG3"] = "/img/12.jpg";
            return View(db_products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
