using Application.Exceptions;
using Application.Mapper;
using BCrypt.Net;
using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Application.Exceptions.UserExceptions;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.DTO;

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

    public async Task<UserDto> GetUserProfile(Guid id)
    {
        var userById = await _repository.GetUserById(id);

		if (userById is null) throw new UserNotFoundException("User Not Found");

		var userDto = UserMapper.ToUserDto(userById);

		return userDto;
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



		

	public async Task<string> Register(RegisterRequest registerRequest)
	{
		var userByEmail = await _repository.IsUserExists(registerRequest.Email);
				
		if (userByEmail) throw new UserExistsException("User already exists") ;
				
		var hashPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
				
		var newUser = User.GenerateNewUser(registerRequest.Name, registerRequest.Lastname, registerRequest.Email, hashPassword);
				
		await _repository.CreateUser(newUser);
				
		return "Registration completed successfully";
	}

    public async Task<string> UploadUserAvatar(IFormFile file, Guid id)
    {

		var userById = await _repository.GetUserById(id);
		if (userById is null) throw new UserNotFoundException("User not found");
		var imageBytes = ImageMapper.ImageToByteArray(file);


		var baseString = Convert.ToBase64String(imageBytes);

		await _repository.UploadUserAvatar(userById.Id, baseString);

		return   "Avatar uploaded successfully";
    }
}
