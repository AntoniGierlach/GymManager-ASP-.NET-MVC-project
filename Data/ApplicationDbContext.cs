using GymManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Membership> Memberships => Set<Membership>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Membership>(entity =>
            {
                entity.Property(m => m.Price).HasColumnType("decimal(10,2)");
                entity.HasIndex(m => m.Name);
            });

            builder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.MembershipId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Enrollments)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Membership)
                      .WithMany(m => m.Enrollments)
                      .HasForeignKey(e => e.MembershipId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
