﻿using Wild_Deer.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Wild_Deer.Models;
namespace Wild_Deer.Controllers
{
    public class AccountController : Controller
    {
        [BindProperty]
        public SignInDto Infos { get; set; }
        [BindProperty]
        public Customer new_customer { get; set; }
        [BindProperty]
        public Seller new_seller { get; set; }

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


   
        public async Task<IActionResult> SigninPost()
        {


            //get UserID If Valid
            int UserID = 1;
            string username = "who";




            var claims = new List<Claim> {
                new Claim("Customer","Customer"),
                new Claim("UserID",UserID.ToString()),
                new Claim("Username",username)
                };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuth", claimPrincipal);
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> SignUpCustommerPost()
        {
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> SignUpSellerPost()
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
