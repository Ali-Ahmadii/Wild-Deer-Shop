using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Wild_Deer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //?
            return View();
        }
        public async Task< IActionResult> SignInAdmin(string username,string password)
        {
            if(username == "Ali" && password == "Ali")
            {
                var claims = new List<Claim> {
                new Claim("Admin","Ali"),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
                return Redirect("/Swagger");
            }
            else
            {
                return Redirect("Index");
            }
 
        }

        [Authorize(Policy = "ItsMe")]
        public async Task<IActionResult> SignOutAdmin()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
