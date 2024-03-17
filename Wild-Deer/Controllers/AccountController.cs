using Wild_Deer.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Wild_Deer.Controllers
{
    public class AccountController : Controller
    {
        [BindProperty]
        public SignInDto Infos { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult Profile()
        {
            //check if no role defiend redirect to sign in
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SigninPost()
        {
            //Here We Must Create Our Claims
            var claims = new List<Claim> {
                new Claim("Customer","Customer"),
                };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> SignupPost()
        {
            return Redirect("/");
        }


        [HttpGet]
        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult EditProfile()
        {
            return null;
        }

        [HttpPost]
        [Authorize(Policy = "SignedInCustomer")]
        public async Task<IActionResult> EditProfilePost()
        {
            return Redirect("/");
        }

        [HttpGet]
        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult BuyedProducts()
        {
            return null;
        }

        [Authorize(Policy = "SignedInCustomer")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


    }
}
