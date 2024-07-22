using Application.DTO;
using Application.DTO.Request;
using Application.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Application;

public interface IUserService
{
	Task<LoginResponse> Login(LoginRequest loginRequest);
	
	Task<string> Register(RegisterRequest registerRequest);
	
	Task<UserDto> GetUserProfile(Guid id);

	Task<string> UploadUserAvatar(IFormFile file,Guid id);

	
}
