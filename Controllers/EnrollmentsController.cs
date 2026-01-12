using GymManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EnrollmentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Enrollments
                .Include(e => e.User)
                .Include(e => e.Membership)
                .OrderByDescending(e => e.PurchasedAt)
                .ToListAsync();

            return View(items);
        }
    }
}
