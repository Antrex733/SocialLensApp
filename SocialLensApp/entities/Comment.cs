namespace SocialLensApp.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public int ReplyAmount { get; set; }
        public List<Comment> ReplyList { get; set; } = new();
        public int PostId { get; set; }
    }
}
