using Application.DTO.Request;
using Application.DTO.Response;

namespace Application;

public interface IUserService
{
	Task<LoginResponse> Login(LoginRequest loginRequest);
	
	Task<string> Register(RegisterRequest registerRequest);
	
	Task<string> LogOut();
}
