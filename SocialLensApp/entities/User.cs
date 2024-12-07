namespace SocialLensApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public int? FollowersAmount { get; set; } = 0;
        public DateTime? DateOfBirth { get; set; }
        public int FriendsAmount { get; set; } = 0;
        public int FollowedAmount { get; set; } = 0;
        public int PostAmount { get; set; } = 0;
        public string? Description { get; set; }
        public List<int> FriendsList { get; set; } = new();
        public List<int> FollowersList { get; set; } = new();
        public List<int> FollowedList { get; set; } = new();
        public List<Post> PostList { get; set; } = new();
        public List<int> BlockedList { get; set; } = new();
        public List<Invite> InvitesList { get; set; } = new();
        
    }
}
