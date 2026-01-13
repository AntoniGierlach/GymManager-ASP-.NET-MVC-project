using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class Club
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa placówki jest wymagana")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Nazwa musi mieć od 2 do 80 znaków")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres jest wymagany")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Adres musi mieć od 5 do 200 znaków")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon kontaktowy jest wymagany")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Telefon musi mieć od 5 do 30 znaków")]
        public string ContactPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email kontaktowy jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        [StringLength(120)]
        public string ContactEmail { get; set; } = string.Empty;

        public ICollection<MembershipClub> MembershipClubs { get; set; } = new List<MembershipClub>();
    }
}