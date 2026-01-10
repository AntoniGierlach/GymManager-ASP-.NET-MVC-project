using GymManager.Models;

public class Enrollment
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int GymClassId { get; set; }
    public GymClass GymClass { get; set; }
}
