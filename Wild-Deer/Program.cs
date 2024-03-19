using Wild_Deer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();


builder.Services.AddStackExchangeRedisCache(action => {
    var connection = "localhost:6379"; //the redis connection
    action.Configuration = connection;
});


builder.Services.AddEntityFrameworkSqlServer().AddDbContext<WildDeerContext>(config =>
{
    config.UseSqlServer("ConnectionString");
});



builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Signin";
});



builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("FromHR", policy => policy.RequireClaim("HR", "Basha"));
});


builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("ItsMe", policy => policy.RequireClaim("Admin", "Ali"));
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("SignedInCustomer", policy => policy.RequireClaim("Customer", "Customer"));
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("SignedInSeller", policy => policy.RequireClaim("Seller", "Seller"));
});

//hide connection string
//builder.Configuration.AddJsonFile("DBConnectionString.json", optional: true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();