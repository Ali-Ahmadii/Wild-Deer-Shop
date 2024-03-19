using Wild_Deer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Wild_Deer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WildDeerContext _dbcontext;
        private readonly IDistributedCache redis;
        public HomeController(ILogger<HomeController> logger, WildDeerContext context,IDistributedCache redis)
        {
            _logger = logger;
            this._dbcontext = context;
            this.redis = redis;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> db_products = _dbcontext.Products.ToList();
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;

            if (nameClaim != null)
            {
                ViewBag.SignInStatus = true;
                ViewBag.Name = userName;
            }
            List<Product> products = _dbcontext.Products.ToList();
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
