namespace SocialLensApp.Entities
{
    public class Invite
    {
        public int Id { get; set; }
        public User InvitingUser { get; set; }
        public int UserId { get; set; }
    }
}
