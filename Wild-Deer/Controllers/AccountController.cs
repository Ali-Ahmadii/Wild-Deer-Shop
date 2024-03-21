using Wild_Deer.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Wild_Deer.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace Wild_Deer.Controllers
{
    public class AccountController : Controller
    {
        [BindProperty]
        public Customer new_customer { get; set; }
        [BindProperty]
        public Seller new_seller { get; set; }

        private WildDeerContext _db;
        private readonly IMemoryCache cache;


        public AccountController(WildDeerContext db,IMemoryCache cha)
        {
            _db = db;
            cache = cha;
        }

        [HttpGet]
        public IActionResult SignUpCustomer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUpSeller()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignIn()
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

        public IActionResult SignInFailed()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SigninPost(string username,string pass)
        {
            //Is This Available In Customer Or Seller
            bool available = true;
                Customer In_Customers = _db.Customers.Where(c => c.Username == username).FirstOrDefault();
                 Seller In_Sellers = _db.Sellers.Where(c => c.Username == username).FirstOrDefault();
            if (In_Customers !=null || In_Sellers != null)
                {
                    if(In_Customers != null)
                    {
                        
                        string password_from_db = In_Customers.HashedPassword;
                        bool password_truness = BCrypt.Net.BCrypt.Verify(pass, password_from_db);
                        if (password_truness)
                        {
                            int UserID = In_Customers.UserId;
                            //Create Its Own Claims
                            var claims = new List<Claim> {
                            new Claim("Customer","Customer"),
                            new Claim("UserID",UserID.ToString()),
                            new Claim("Username",username)
                            };
                            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
                            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
                            cache.Set(0, true);
                            cache.Set(1, In_Customers.FirstName);
                            
                        return Redirect("/Home/Index");
                            //Redirect
                        }
                        else
                        {
                            ViewData["Status"] = "Failed";
                            return View("SignIn");
                        }
                        //If Available In Customers
                    }
                    else
                    {
                        
                        string password_from_db = In_Sellers.PasswordHash;
                        bool password_truness = BCrypt.Net.BCrypt.Verify(pass, password_from_db);
                        if (password_truness)
                        {
                            int SellerID = In_Sellers.SellerId;
                            //Create Its Own Claims
                            var claims = new List<Claim> {
                                new Claim("Seller","Seller"),
                                new Claim("SellerID",SellerID.ToString()),
                                new Claim("Username",username)
                                };
                            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
                            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
                        cache.Set(0, true);
                        cache.Set(1, In_Sellers.OwnerName);
                        return Redirect("/");
                            //Redirect
                        }
                        else
                        {
                            ViewData["Status"] = "Failed";
                            return View("SignIn");
                        }
                }
                }
                else
                {
                ViewData["Status"] = "Failed";
                return View("SignIn");
            }

            //Redirect




        }

        private bool UniqueUser()
        {
            int In_Customers = _db.Customers.Count(c=>c.Username == new_customer.Username);
            int In_Sellers = _db.Sellers.Count(c=>c.Username == new_customer.Username);
            if(In_Customers == 1 || In_Sellers == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
            //Customer customers = _db.Customers.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpCustomerPost()
        {

            //check if username is not repeated in seller and customer pages if repeated get back to page
            bool is_unique = UniqueUser();
            if (is_unique)
            {
                string hashed_password = BCrypt.Net.BCrypt.HashPassword(new_customer.HashedPassword);

                //hash given password
                new_customer.HashedPassword = hashed_password;
                //save to database
                _db.Customers.Add(new_customer);
                savechanges();
                //create claims

                int UserID = new_customer.UserId;
                var claims = new List<Claim> {
                new Claim("Customer","Customer"),
                new Claim("UserID",UserID.ToString()),
                new Claim("Username",new_customer.Username)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);

                //delete object

                cache.Set(0, true);
                cache.Set(1, new_customer.FirstName);
                new_customer = null;
                Redirect("/");
                //move to first page
            }
            else
            {
                SignUpFailed();
            }


            return Redirect("/");
        }

        public IActionResult SignUpFailed()
        {
            ViewData["Status"] = "Fail";
            return View("SignUpCustomer");
        }

        [HttpPost]
        public async Task<IActionResult> SignUpSellerPost()
        {
            //check if username is not repeated in seller and customer pages if repeated get back to page
            bool is_unique = UniqueUser();
            if (is_unique)
            {
                string hashed_password = BCrypt.Net.BCrypt.HashPassword(new_seller.PasswordHash);

                //hash given password
                new_seller.PasswordHash = hashed_password;
                //save to database
                _db.Sellers.Add(new_seller);
                savechanges();
                //create claims
                int SellerID = new_seller.SellerId ;
                var claims = new List<Claim> {
                                new Claim("Seller","Seller"),
                                new Claim("SellerID",SellerID.ToString()),
                                new Claim("Username",new_seller.Username)
                                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);

                cache.Set(0, true);
                cache.Set(1, new_seller.OwnerName);
                new_seller = null;
                //delete object
                return Redirect("/");
                //move to first page
            }
            else
            {
                SignUpellerFailed();
            }


            return Redirect("/");
        }
        public IActionResult SignUpellerFailed()
        {
            ViewData["Status"] = "Fail";
            return View("SignUpCustomer");
        }



        [Authorize(Policy = "SignedInCustomer")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            cache.Remove(0);
            cache.Remove(1);
            return Redirect("/");
        }


        private void savechanges()
        {
            _db.SaveChanges();
        }

    }
}
