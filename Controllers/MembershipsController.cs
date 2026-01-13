using GymManager.Data;
using GymManager.Models;
using GymManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MembershipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var memberships = await _context.Memberships
                .Include(m => m.MembershipClubs)
                    .ThenInclude(mc => mc.Club)
                .ToListAsync();

            return View(memberships);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var membership = await _context.Memberships
                .Include(m => m.MembershipClubs)
                    .ThenInclude(mc => mc.Club)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return NotFound();

            return View(membership);
        }

        public async Task<IActionResult> Create()
        {
            var clubs = await _context.Clubs.OrderBy(c => c.Name).ToListAsync();
            var vm = new MembershipFormVM
            {
                IsOpenAll = true,
                Clubs = clubs
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembershipFormVM vm)
        {
            vm.Clubs = await _context.Clubs.OrderBy(c => c.Name).ToListAsync();

            if (!vm.IsOpenAll && (vm.SelectedClubIds == null || vm.SelectedClubIds.Count == 0))
                ModelState.AddModelError("", "Wybierz co najmniej jeden oddział albo ustaw OPEN (wszystkie oddziały).");

            if (!ModelState.IsValid)
                return View(vm);

            var membership = new Membership
            {
                Name = vm.Name,
                Price = vm.Price,
                DurationInDays = vm.DurationInDays,
                IsOpenAll = vm.IsOpenAll
            };

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            if (!membership.IsOpenAll)
            {
                var selectedIds = (vm.SelectedClubIds ?? new List<int>()).Distinct().ToList();

                var links = selectedIds
                    .Select(clubId => new MembershipClub { MembershipId = membership.Id, ClubId = clubId })
                    .ToList();

                _context.MembershipClubs.AddRange(links);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var membership = await _context.Memberships
                .Include(m => m.MembershipClubs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return NotFound();

            var clubs = await _context.Clubs.OrderBy(c => c.Name).ToListAsync();
            var selected = membership.MembershipClubs.Select(mc => mc.ClubId).ToList();

            var vm = new MembershipFormVM
            {
                Id = membership.Id,
                Name = membership.Name,
                Price = membership.Price,
                DurationInDays = membership.DurationInDays,
                IsOpenAll = membership.IsOpenAll,
                SelectedClubIds = selected,
                Clubs = clubs
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MembershipFormVM vm)
        {
            if (vm.Id == null || id != vm.Id.Value) return NotFound();

            vm.Clubs = await _context.Clubs.OrderBy(c => c.Name).ToListAsync();

            if (!vm.IsOpenAll && (vm.SelectedClubIds == null || vm.SelectedClubIds.Count == 0))
                ModelState.AddModelError("", "Wybierz co najmniej jeden oddział albo ustaw OPEN (wszystkie oddziały).");

            if (!ModelState.IsValid)
                return View(vm);

            var membership = await _context.Memberships
                .Include(m => m.MembershipClubs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return NotFound();

            membership.Name = vm.Name;
            membership.Price = vm.Price;
            membership.DurationInDays = vm.DurationInDays;
            membership.IsOpenAll = vm.IsOpenAll;

            _context.MembershipClubs.RemoveRange(membership.MembershipClubs);

            if (!membership.IsOpenAll)
            {
                var selectedIds = (vm.SelectedClubIds ?? new List<int>()).Distinct().ToList();

                var links = selectedIds
                    .Select(clubId => new MembershipClub { MembershipId = membership.Id, ClubId = clubId })
                    .ToList();

                _context.MembershipClubs.AddRange(links);
            }

            _context.Update(membership);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var membership = await _context.Memberships
                .Include(m => m.MembershipClubs)
                    .ThenInclude(mc => mc.Club)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return NotFound();

            return View(membership);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null) return NotFound();

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
