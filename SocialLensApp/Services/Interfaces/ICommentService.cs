using SocialLensApp.Models;

namespace SocialLensApp.Services.Interfaces
{
    public interface ICommentService
    {
        void CreateComment(int postId, CommentDto createCommentDto);
        void EditComment(int id, CommentDto editCommentDto);
        void LikeComment(int id);
        void DislikeComment(int id);
        void ReplyComment(int id, CommentDto replyCommentDto);
        void DeleteComment(int id);
    }
}
