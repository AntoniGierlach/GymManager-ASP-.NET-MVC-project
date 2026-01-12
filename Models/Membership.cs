using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class Membership
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa karnetu jest wymagana")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(0.01, 10000, ErrorMessage = "Cena musi być większa od 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Czas trwania jest wymagany")]
        [Range(1, 365, ErrorMessage = "Czas trwania musi być od 1 do 365 dni")]
        public int DurationInDays { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
