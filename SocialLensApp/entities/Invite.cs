namespace SocialLensApp.Entities
{
    public class Invite
    {
        public int Id { get; set; }
        public int InvitingUser { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
