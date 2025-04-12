namespace SocialLensApp.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int LikeAmount {  get; set; }
        public int DislikeAmount { get; set; }
        public int PostCreatorId { get; set; }
    }
}
