using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Wild_Deer.Controllers
{
    public class HRController : Controller
    {
        [Authorize(Policy = "FromHR")]
        public IActionResult Index()
        {
            return Ok();
        }


        public async Task<IActionResult> SignInHR()
        {
            //some if
            var claims = new List<Claim> { 
                new Claim("HR","Basha"),
                };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
            return Redirect("/HR");
        }

        [Authorize(Policy = "FromHR")]
        public async Task<IActionResult> SignOutHR()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
