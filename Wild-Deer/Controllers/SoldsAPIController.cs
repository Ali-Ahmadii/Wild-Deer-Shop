using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize(Policy ="ItsMe")]
    [Route("api/[controller]")]
    [ApiController]
    public class SoldsAPIController : ControllerBase
    {
        private readonly WildDeerContext _context;

        public SoldsAPIController(WildDeerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sold>>> GetSolds()
        {
            if (_context.Solds == null)
            {
                return NotFound();
            }
            return await _context.Solds.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Sold>> GetSold(int id)
        {
            if (_context.Solds == null)
            {
                return NotFound();
            }
            var sold = await _context.Solds.FindAsync(id);

            if (sold == null)
            {
                return NotFound();
            }

            return sold;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSold(int id, Sold sold)
        {
            if (id != sold.SellInfoId)
            {
                return BadRequest();
            }

            _context.Entry(sold).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoldExists(id))
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
        public async Task<ActionResult<Sold>> PostSold(Sold sold)
        {
            if (_context.Solds == null)
            {
                return Problem("Entity set '_db.Solds'  is null.");
            }
            _context.Solds.Add(sold);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSold", new { id = sold.SellInfoId }, sold);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSold(int id)
        {
            if (_context.Solds == null)
            {
                return NotFound();
            }
            var sold = await _context.Solds.FindAsync(id);
            if (sold == null)
            {
                return NotFound();
            }

            _context.Solds.Remove(sold);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SoldExists(int id)
        {
            return (_context.Solds?.Any(e => e.SellInfoId == id)).GetValueOrDefault();
        }
    }
}
