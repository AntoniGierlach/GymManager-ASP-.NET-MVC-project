namespace GymManager.Models
{
    public class MembershipClub
    {
        public int MembershipId { get; set; }
        public Membership Membership { get; set; } = default!;

        public int ClubId { get; set; }
        public Club Club { get; set; } = default!;
    }
}
