using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy = "ItsMe")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsAPIController : ControllerBase
    {
        private readonly WildDeerContext _context;

        public CommentsAPIController(WildDeerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommnets()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Customer comment)
        {
            if (id != comment.UserId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Comment>> PostComments(Comment comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set '_db.Comments'  is null.");
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comment.CommentId }, comment);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentsExists(int id)
        {
            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
