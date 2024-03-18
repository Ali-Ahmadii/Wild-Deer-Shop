using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Wild_Deer.Models;
using Wild_Deer.Models.Dto;

namespace Wild_Deer.Controllers
{
    public class CommentController : Controller
    {
        [BindProperty]
        public SignInDto MyProperty { get; set; }
        private WildDeerContext _db;
        public CommentController(WildDeerContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Policy = "SignedInCustomer")]
        public IActionResult WriteComment(int ProductID,float score,string Descript)
        {
            string referrerUrl = HttpContext.Request.Headers["Referer"].ToString();
            var user = HttpContext.User;
            var nameClaim = user.Claims.FirstOrDefault(c=>c.Type == "UserID")?.Value;
            var UsernameClaim = user.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            int userID = Convert.ToInt32(nameClaim);
            Comment a_comment = new Comment();
            a_comment.Writer = _db.Customers.FirstOrDefault(cc=>cc.UserId ==userID);
            a_comment.Product = _db.Products.FirstOrDefault(cc => cc.ProductId == ProductID);
            a_comment.WriterId = userID;
            a_comment.ProductId = ProductID;
            a_comment.Score = score;
            a_comment.Description = Descript;
            a_comment.Username = UsernameClaim.ToString();
            _db.Comments.Add(a_comment);
            savechanges();
            return Redirect(referrerUrl);
        }
        private void savechanges()
        {
            _db.SaveChanges();
        }


    }
}
