using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int GymClassId { get; set; }

        public GymClass GymClass { get; set; }
    }
}
