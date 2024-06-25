using Application.Exceptions;
using Application.Mapper;
using BCrypt.Net;
using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Application.Exceptions.UserExceptions;
using Application.DTO.Request;
using Application.DTO.Response;

namespace Application;

public class UserService : IUserService

{
	
	private readonly IUserRepository _repository;
	
	private readonly ITokenService _tokenService;
	
	
	
	public UserService(IUserRepository repository, ITokenService tokenService)
	{
		_repository =  repository;
		_tokenService = tokenService;
		
	}

	public async Task<LoginResponse> Login(LoginRequest loginRequest)
	{

		var userByEmail = await  _repository.GetUserByEmail(loginRequest.Email);

        if (userByEmail is null) throw new UserNotFoundException("User not found");
				
		var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginRequest.Password, userByEmail.Password);
				
		if (!isPasswordCorrect) throw new InvalidUserCredentials("Invalid email or password");
				
		var token = _tokenService.GenerateJwtToken(userByEmail.Id, userByEmail.Name);

				
				
		

        


        return new LoginResponse()
		{
			Message = "Login is successful",
			User = UserMapper.ToUserDto(userByEmail),
			Token = token
        } ;
	}

	public Task<string> LogOut()
	{
		throw new NotImplementedException();
	}

		

	public async Task<string> Register(RegisterRequest registerRequest)
	{
		var userByEmail = await _repository.GetUserByEmail(registerRequest.Email);
				
		if (userByEmail is not null) throw new UserExistsException("User already exists") ;
				
		var hashPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
				
		var newUser = User.GenerateNewUser(registerRequest.Name, registerRequest.Lastname, registerRequest.Email, hashPassword);
				
		await _repository.CreateUser(newUser);
				
		return "Registration completed successfully";
	}

	
}
