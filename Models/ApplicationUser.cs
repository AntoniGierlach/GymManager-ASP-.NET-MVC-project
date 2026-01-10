using Microsoft.AspNetCore.Identity;
namespace GymManager.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Enrollment> Enrollments { get; set; }
}
