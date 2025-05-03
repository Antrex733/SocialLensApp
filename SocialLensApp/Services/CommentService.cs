using AutoMapper;
using SocialLensApp.Data;
using SocialLensApp.Entities;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;
namespace SocialLensApp.Services
{
    public class CommentService : ICommentService
    {
        private IUserContextService _contextAccessor;
        private IMapper _mapper;
        private SocialLensDbContext _context;

        public CommentService(IUserContextService contextAccessor, IMapper mapper, SocialLensDbContext context)
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _context = context;
        }
        public void CreateComment(int postId, CommentDto createCommentDto)
        {
            var UserId = _contextAccessor.getUserId;
            var addComment = _mapper.Map<Comment>(createCommentDto);
            var post = _context.Posts.FirstOrDefault(i => i.Id == postId);
            
            post.CommentAmount++;
            post.CommentList.Add(addComment);

            addComment.CreatorId = (int)UserId;
            addComment.PostId = postId;
            addComment.ReplyAmount = 0;
            addComment.likeAmmount = 0;
            addComment.dislikeAmmount = 0;

            _context.Add(addComment);
            _context.SaveChanges();
        }

        public void EditComment(int id, CommentDto editCommentDto)
        {
            var comment = _context.Comments.FirstOrDefault(i => i.Id == id);
            
            comment.Content = editCommentDto.Content;
            _context.SaveChanges();
        }

        public void LikeComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(i => i.Id == id);
            
            comment.likeAmmount++;
            _context.SaveChanges();
        }

        public void DislikeComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(i => i.Id == id);

            comment.dislikeAmmount++;
            _context.SaveChanges();
        }

        public void ReplyComment(int id, CommentDto replyCommentDto)
        {
            var UserId = _contextAccessor.getUserId;
            var addComment = _mapper.Map<Comment>(replyCommentDto);
            var comment = _context.Comments.FirstOrDefault(i => i.Id == id);

            comment.ReplyAmount++;
            comment.ReplyList.Add(addComment);

            addComment.CreatorId = (int)UserId;
            addComment.ReplyAmount = 0;
            addComment.likeAmmount = 0;
            addComment.dislikeAmmount = 0;
            addComment.ParentCommentId = id;
            addComment.PostId = comment.PostId;

            _context.Add(addComment);
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(i => i.Id == id);
            var post = _context.Posts.FirstOrDefault(i => i.Id == comment.PostId);
            var parentComment = _context.Comments.FirstOrDefault(i => i.Id == comment.ParentCommentId);
            var replies = _context.Comments.Where(i => i.ParentCommentId == id).ToList();
            
            parentComment.ReplyAmount--;
            post.CommentAmount--;
            
            _context.RemoveRange(replies);
            _context.Remove(comment);
            _context.SaveChanges();
        }
    }
}
