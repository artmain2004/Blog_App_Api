using Application;
using Application.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace blog_app.Controllers
{
    [Route("api/users")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

       
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }


        [HttpPost("login")]
        
        public async Task<ActionResult> UserLogin([FromBody] LoginRequest loginRequest)
        {
            var result = await _userService.Login(loginRequest);

            return Ok(result);
        }  
        
        [HttpPost("register")]
        

        public async Task<ActionResult> UserRegister([FromBody] RegisterRequest registerRequest)
        {
            var result = await _userService.Register(registerRequest);

            return Ok(result);
        }

       

        [HttpPost("upload/{id}")]
        public async Task<ActionResult> UploadUserAvatar([FromForm] IFormFile avatar, [FromRoute] Guid id)
        {
            if (avatar == null)
            {
                return BadRequest("File not found");
            }

            var result = await _userService.UploadUserAvatar(avatar,id);

            return Ok(result);
        }


        [HttpGet("profile/{id}")]

        public async Task<ActionResult> GetUserProfile([FromRoute] Guid id)
        {
            var result = await _userService.GetUserProfile(id);

            return Ok(result);
        }

        

        

       


    }
}
