using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;

        [Required]
        public int MembershipId { get; set; }

        public Membership Membership { get; set; } = default!;

        [Required]
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}
