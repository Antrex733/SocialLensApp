namespace SocialLensApp.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int? FollowersAmount { get; set; }
        public int FriendsAmount { get; set; } 
        public int FollowedAmount { get; set; } 
        public int PostAmount { get; set; }
        public string? Description { get; set; }
    }
}
