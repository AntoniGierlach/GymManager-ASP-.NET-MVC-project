using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MembershipsApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        public class MembershipDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int DurationInDays { get; set; }
            public bool IsOpenAll { get; set; }

            public List<int> ClubIds { get; set; } = new();
            public List<string> ClubNames { get; set; } = new();
        }

        public class MembershipCreateUpdateDto
        {
            [Required, StringLength(50, MinimumLength = 3)]
            public string Name { get; set; } = string.Empty;

            [Range(0.01, 10000)]
            public decimal Price { get; set; }

            [Range(1, 365)]
            public int DurationInDays { get; set; }

            public bool IsOpenAll { get; set; } = true;
            public List<int> ClubIds { get; set; } = new();
        }

        // GET: /api/membershipsapi
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MembershipDto>>> GetAll()
        {
            var items = await _db.Memberships
                .Include(m => m.MembershipClubs)
                    .ThenInclude(mc => mc.Club)
                .OrderBy(m => m.Name)
                .ToListAsync();

            var result = items.Select(ToDto).ToList();
            return Ok(result);
        }

        // GET: /api/membershipsapi/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MembershipDto>> GetById(int id)
        {
            var m = await _db.Memberships
                .Include(x => x.MembershipClubs)
                    .ThenInclude(mc => mc.Club)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (m == null) return NotFound();

            return Ok(ToDto(m));
        }

        // POST: /api/membershipsapi
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MembershipDto>> Create([FromBody] MembershipCreateUpdateDto dto)
        {
            if (!dto.IsOpenAll && (dto.ClubIds == null || dto.ClubIds.Count == 0))
                return BadRequest(new { error = "Wybierz co najmniej jeden oddział albo ustaw IsOpenAll=true." });

            var membership = new Membership
            {
                Name = dto.Name,
                Price = dto.Price,
                DurationInDays = dto.DurationInDays,
                IsOpenAll = dto.IsOpenAll
            };

            _db.Memberships.Add(membership);
            await _db.SaveChangesAsync();

            if (!membership.IsOpenAll)
            {
                var distinctClubIds = (dto.ClubIds ?? new List<int>()).Distinct().ToList();

                var existingClubIds = await _db.Clubs
                    .Where(c => distinctClubIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (existingClubIds.Count != distinctClubIds.Count)
                    return BadRequest(new { error = "Jeden lub więcej oddziałów nie istnieje." });

                var links = existingClubIds
                    .Select(cid => new MembershipClub { MembershipId = membership.Id, ClubId = cid })
                    .ToList();

                _db.MembershipClubs.AddRange(links);
                await _db.SaveChangesAsync();
            }

            var created = await _db.Memberships
                .Include(m => m.MembershipClubs).ThenInclude(mc => mc.Club)
                .FirstAsync(m => m.Id == membership.Id);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToDto(created));
        }

        // PUT: /api/membershipsapi/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MembershipDto>> Update(int id, [FromBody] MembershipCreateUpdateDto dto)
        {
            if (!dto.IsOpenAll && (dto.ClubIds == null || dto.ClubIds.Count == 0))
                return BadRequest(new { error = "Wybierz co najmniej jeden oddział albo ustaw IsOpenAll=true." });

            var membership = await _db.Memberships
                .Include(m => m.MembershipClubs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return NotFound();

            membership.Name = dto.Name;
            membership.Price = dto.Price;
            membership.DurationInDays = dto.DurationInDays;
            membership.IsOpenAll = dto.IsOpenAll;

            _db.MembershipClubs.RemoveRange(membership.MembershipClubs);

            if (!membership.IsOpenAll)
            {
                var distinctClubIds = (dto.ClubIds ?? new List<int>()).Distinct().ToList();

                var existingClubIds = await _db.Clubs
                    .Where(c => distinctClubIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (existingClubIds.Count != distinctClubIds.Count)
                    return BadRequest(new { error = "Jeden lub więcej oddziałów nie istnieje." });

                var links = existingClubIds
                    .Select(cid => new MembershipClub { MembershipId = membership.Id, ClubId = cid })
                    .ToList();

                _db.MembershipClubs.AddRange(links);
            }

            await _db.SaveChangesAsync();

            var updated = await _db.Memberships
                .Include(m => m.MembershipClubs).ThenInclude(mc => mc.Club)
                .FirstAsync(m => m.Id == membership.Id);

            return Ok(ToDto(updated));
        }

        // DELETE: /api/membershipsapi/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var membership = await _db.Memberships.FindAsync(id);
            if (membership == null) return NotFound();

            _db.Memberships.Remove(membership);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private static MembershipDto ToDto(Membership m)
        {
            var clubs = m.MembershipClubs?
                .Select(x => x.Club)
                .Where(c => c != null)
                .ToList() ?? new List<Club>();

            return new MembershipDto
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                DurationInDays = m.DurationInDays,
                IsOpenAll = m.IsOpenAll,
                ClubIds = clubs.Select(c => c.Id).ToList(),
                ClubNames = clubs.Select(c => c.Name).ToList()
            };
        }
    }
}
