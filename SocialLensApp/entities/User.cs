namespace SocialLensApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public int? FollowersAmount { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int FriendsAmount { get; set; }
        public int FollowedAmount { get; set; }
        public int PostAmount { get; set; }
        public string? Description { get; set; }
        public List<User> FriendsList { get; set; } = new();
        public List<User> FollowersList { get; set; } = new();
        public List<User> FollowedList { get; set; } = new();
        public List<Post> PostList { get; set; } = new();
        public List<User> BlockedList { get; set; } = new();
        public List<Invite> InvitesList { get; set; } = new();
        
    }
}
