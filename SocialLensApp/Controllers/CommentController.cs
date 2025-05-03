using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {

        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("createcomment/{id}")]
        public IActionResult CreateComment([FromRoute] int id, [FromBody] CommentDto createCommentDto)
        {
            _commentService.CreateComment(id, createCommentDto);
            return Created();
        }

        [HttpPatch("editcomment/{id}")]
        public IActionResult EditComment([FromRoute] int id, [FromBody] CommentDto editCommentDto)
        {
            _commentService.EditComment(id, editCommentDto);
            return Ok();
        }

        [HttpPatch("likecomment/{id}")]
        public IActionResult LikeComment([FromRoute] int id)
        {
            _commentService.LikeComment(id);
            return Ok();
        }

        [HttpPatch("dislikecomment/{id}")]
        public IActionResult DislikeComment([FromRoute] int id)
        {
            _commentService.DislikeComment(id);
            return Ok();
        }

        [HttpPost("replycomment/{id}")]
        public IActionResult ReplyComment([FromRoute] int id, [FromBody] CommentDto replyCommentDto)
        {
            _commentService.ReplyComment(id, replyCommentDto);
            return Created();
        }

        [HttpDelete("deletecomment/{id}")]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            _commentService.DeleteComment(id);
            return NoContent();
        }

    }
}
