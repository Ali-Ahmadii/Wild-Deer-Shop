using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Wild_Deer.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Policy = "ItsMe")]
        public IActionResult Index()
        {
            //?
            return View();
        }

        public IActionResult HRSection()
        {
            return Ok();
        }
        public IActionResult Products()
        {
            return Ok();
        }
        public IActionResult Customer()
        {
            return Ok();
        }
        public IActionResult SoldSection()
        {
            return Ok();
        }
        public async Task< IActionResult> SignInAdmin()
        {
            //some if
            var claims = new List<Claim> {
                new Claim("Admin","Ali"),
                };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
            return Redirect("/Admin");
        }


        public async Task<IActionResult> SignOutAdmin()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
