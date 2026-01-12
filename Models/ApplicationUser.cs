using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50, ErrorMessage = "Imię może mieć max 50 znaków")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Nazwisko może mieć max 50 znaków")]
        public string? LastName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
