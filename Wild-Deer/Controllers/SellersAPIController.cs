using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wild_Deer.Models;

namespace Wild_Deer.Controllers
{
    [Authorize("ItsMe")]
    [Route("api/[controller]")]
    [ApiController]
    public class SellersAPIController : ControllerBase
    {
        private readonly WildDeerContext _context;

        public SellersAPIController(WildDeerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Sellers.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            if (_context.Sellers == null)
            {
                return NotFound();
            }
            var seller = await _context.Sellers.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, Seller seller)
        {
            if (id != seller.SellerId)
            {
                return BadRequest();
            }

            _context.Entry(seller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(id))
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
        public async Task<ActionResult<Seller>> PostSeller(Seller seller)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set '_db.Sellers'  is null.");
            }
            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeller", new { id = seller.SellerId }, seller);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            if (_context.Sellers == null)
            {
                return NotFound();
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerExists(int id)
        {
            return (_context.Sellers?.Any(e => e.SellerId == id)).GetValueOrDefault();
        }
    }
}
