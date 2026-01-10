using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MembershipsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: api/memberships
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membership>>> GetMemberships()
        {
            return Ok(await _context.Memberships.ToListAsync());
        }

        // =========================
        // GET: api/memberships/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Membership>> GetMembership(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
                return NotFound();

            return Ok(membership);
        }

        // =========================
        // POST: api/memberships
        // =========================
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Membership>> CreateMembership(Membership membership)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMembership),
                new { id = membership.Id },
                membership
            );
        }

        // =========================
        // PUT: api/memberships/5
        // =========================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMembership(int id, Membership membership)
        {
            if (id != membership.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _context.Memberships.AnyAsync(m => m.Id == id);
            if (!exists)
                return NotFound();

            _context.Entry(membership).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================
        // DELETE: api/memberships/5
        // =========================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
                return NotFound();

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
