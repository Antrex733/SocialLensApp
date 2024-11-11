namespace SocialLensApp.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int LikeAmount { get; set; }
        public int DislikeAmount { get; set; }
        public int CommentAmount { get; set; }
        public int PostCreatorId { get; set; }
        public List<Comment> CommentList { get; set; } = new();
    }
}
