using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("createpost")]
        public IActionResult CreatePost([FromBody] CreatePostDto createPostDto)
        {
            _postService.CreatePost(createPostDto);
            return Created();
        }

        [HttpGet("searchpost")]
        public IActionResult SearchPost([FromHeader] string searchQuery)
        {
            var results = _postService.SearchPosts(searchQuery);
            return Ok(results);
        }

        [HttpPatch("likepost/{id}")]
        public IActionResult LikePost([FromRoute]int id)
        {
            _postService.LikePost(id);

            return Ok();
        }

        [HttpPatch("dislikepost/{id}")]
        public IActionResult DisLikePost([FromRoute] int id)
        {
            _postService.DisLikePost(id);

            return Ok();
        }

        [HttpPatch("editpost/{id}")]
        public IActionResult EditPost([FromRoute] int id, [FromBody] EditPostDto editPostDto)
        {
            _postService.EditPost(id, editPostDto);
            return Ok();
        }

        [HttpDelete("deletepost/{id}")]
        public IActionResult DeletePost([FromRoute] int id)
        {
            _postService.DeletePost(id);
            return Ok();
        }
    }
}
