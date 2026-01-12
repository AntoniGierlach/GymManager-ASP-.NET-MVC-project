using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var memberships = await _db.Memberships.ToListAsync();
            return View(memberships);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Buy(int membershipId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var membership = await _db.Memberships.FindAsync(membershipId);
            if (membership == null)
                return NotFound();

            bool alreadyBought = await _db.Enrollments
                .AnyAsync(e => e.UserId == user.Id && e.MembershipId == membershipId);

            if (alreadyBought)
            {
                TempData["Error"] = "Ten karnet został już przez Ciebie zakupiony.";
                return RedirectToAction(nameof(Index));
            }

            var enrollment = new Enrollment
            {
                UserId = user.Id,
                MembershipId = membership.Id
            };

            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Karnet został pomyślnie zakupiony.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> My()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var enrollments = await _db.Enrollments
                .Where(e => e.UserId == user.Id)
                .Include(e => e.Membership)
                .OrderByDescending(e => e.PurchasedAt)
                .ToListAsync();

            return View(enrollments);
        }
    }
}
