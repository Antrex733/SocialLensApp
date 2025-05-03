using AutoMapper;
using SocialLensApp;
using SocialLensApp.Data;
using SocialLensApp.Entities;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Services
{
    public class PostService : IPostService
    {
        private IUserContextService _contextAccessor;
        private IMapper _mapper;
        private SocialLensDbContext _context;

        public PostService(IUserContextService contextAccessor, IMapper mapper, SocialLensDbContext context)
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _context = context;
        }
        public void CreatePost(CreatePostDto dto)
        {
            var UserId = _contextAccessor.getUserId;
            var addPost = _mapper.Map<Post>(dto);
            addPost.PostCreatorId = (int)UserId;

            _context.Add(addPost);
            _context.SaveChanges();
        }

        public List<PostDto> SearchPosts(string search)
        {
            search = search.ToLower();
            var posts = _context.Posts.Where(i => i.Title.ToLower().Contains(search) || i.Description.ToLower().Contains(search)).ToList();
            var postdto = _mapper.Map<List<PostDto>>(posts);
            return postdto;
        }

        public void LikePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(i => i.Id == id);
           
            post.LikeAmount++;
            _context.SaveChanges();
        }

        public void DisLikePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(i => i.Id == id);

            post.DislikeAmount++;
            _context.SaveChanges();
        }

        public void EditPost(int id, EditPostDto editPostDto)
        {
            var post = _context.Posts.FirstOrDefault(i => i.Id == id);
            post.Description = editPostDto.Description;
            post.Title = editPostDto.Title;

            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(i => i.Id == id);
            var comments = _context.Comments.Where(i => i.PostId == id).ToList();
            
            _context.RemoveRange(comments);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
