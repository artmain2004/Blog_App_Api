using Application.Service;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.Request;

namespace blog_app.Controllers;

[ApiController]
[Route("api/posts")]

public class PostController : ControllerBase
{

    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPosts()
    {
        var result = await _postService.GetAllPosts();

        return Ok(result);
    }
    
    [HttpGet("title")]
    public async Task<ActionResult> GetAllPostsByTitle([FromQuery] string title)
    {
        var result = await _postService.GetPostsByTitle(title);

        return Ok(result);
    }

    
    [HttpGet("/myposts/{id}")]
    [Authorize]
     public async Task<ActionResult> GetAllPostsById([FromRoute] Guid id)
     {
        var result = await _postService.GetPostsById(id);

         return Ok(result);
     }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetPostById([FromRoute] Guid id)
    {
        var result = await _postService.GetPostById(id);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult> CreatePost([FromBody] PostCreateRequest postCreateRequest)
    {
        var result =  await _postService.CreatePost(postCreateRequest);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult> UpdatePost([FromBody] PostUpdateRequest postUpdateRequest, [FromRoute] Guid id)
    {
        var result = await _postService.UpdatePost(postUpdateRequest, id);

        return Ok(result);
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeletePost([FromRoute] Guid id)
    {
        var result = await _postService.DeletePost(id);

        return Ok(result);
    }
    
    
}