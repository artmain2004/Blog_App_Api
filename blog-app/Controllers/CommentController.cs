using Application.DTO.Request;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog_app.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpPost]
        [Authorize]

        public async Task<ActionResult> CreateComment([FromBody] CreateCommentRequest createCommentRequest, [FromQuery] Guid userId, [FromQuery] Guid postId)
        {
            var result = await _commentService.CreateComment(createCommentRequest, userId, postId);

            return Ok(result);
        }
    }
}
