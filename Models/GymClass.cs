using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class GymClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa zajęć jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może przekroczyć 100 znaków")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Data zajęć jest wymagana")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
