using GymManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Data;

public class ApplicationDbContext 
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Membership> Memberships { get; set; }
    public DbSet<GymClass> GymClasses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}
