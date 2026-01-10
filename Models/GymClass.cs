public class GymClass
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
