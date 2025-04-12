using SocialLensApp.Models;

namespace SocialLensApp.Services.Interfaces
{
    public interface IPostService
    {
        public void CreatePost(CreatePostDto post);
        public List<PostDto> SearchPosts(string search);
        public void LikePost(int id);
        public void DisLikePost(int id);
        public void EditPost(int id, EditPostDto editPostDto);
        public void DeletePost(int id);
    }

}
