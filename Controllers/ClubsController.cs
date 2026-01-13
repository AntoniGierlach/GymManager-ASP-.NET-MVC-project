using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
            => View(await _context.Clubs.OrderBy(c => c.Name).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (club == null) return NotFound();

            return View(club);
        }

        public IActionResult Create() => View(new Club());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Club club)
        {
            if (!ModelState.IsValid) return View(club);

            _context.Add(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var club = await _context.Clubs.FindAsync(id);
            if (club == null) return NotFound();

            return View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Club club)
        {
            if (id != club.Id) return NotFound();
            if (!ModelState.IsValid) return View(club);

            _context.Update(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (club == null) return NotFound();

            return View(club);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null) return NotFound();

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
