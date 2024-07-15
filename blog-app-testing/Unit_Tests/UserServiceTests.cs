using Application;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Exceptions.UserExceptions;
using Application.Mapper;
using Domain.Entity;
using Domain.Interface;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace blog_app_testing.Unit_Tests
{
    public class UserServiceTests
    {
        
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IUserService _userServiceMock;
       

        public UserServiceTests()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _userRepoMock = new Mock<IUserRepository>();
            _userServiceMock = new UserService(_userRepoMock.Object, _tokenServiceMock.Object);
            
        }

        [Fact]
        public async Task  LoginUser_PostCorrectUserCredentials_ReturnLoginResponse()
        {
            var loginRequest = GenerateLoginRequest();
            
            User userByEmail = GenerateUser();


            _userRepoMock.Setup(f => f.GetUserByEmail(loginRequest.Email)).ReturnsAsync(userByEmail);

            _tokenServiceMock.Setup(f => f.GenerateJwtToken(userByEmail.Id, userByEmail.Name)).Returns("token");


            var result = _userServiceMock.Login(loginRequest);


            result.Result.Token.Should().NotBeNull();
            result.Result.Message.Should().Be("Login is successful");
            result.Result.User.Should().NotBeNull();
            result.Result.User.Name.Should().Be("Artsiom");
            
        }


        [Fact]
        public async Task LoginUser_UserIsNotExists_ReturnUserNotFoundException()
        {
            var loginRequest = GenerateLoginRequest();

            _userRepoMock.Setup(repo => repo.GetUserByEmail(loginRequest.Email)).Returns(Task.FromResult<User>(null));

            var exception = Assert.ThrowsAsync<UserNotFoundException>(() => _userServiceMock.Login(loginRequest));

            exception.Result.Message.Should().Be("User not found");


        }
        
        
        [Fact]
        public async Task LoginUser_PostWrongUserCredentials_ReturnInvalidUserCredentials()
        {
            var loginRequest = GenerateWrongLoginRequest();

            var user = GenerateUser();

            _userRepoMock.Setup(repo => repo.GetUserByEmail(loginRequest.Email)).ReturnsAsync(user);

            var exception = Assert.ThrowsAsync<InvalidUserCredentials>(() => _userServiceMock.Login(loginRequest));

            exception.Result.Message.Should().Be("Invalid email or password");


        }

        [Fact]
        public async Task RegisterUser_UsersExists_ReturnUserExistsException()
        {
            var registerRequest = GenerateRegisterRequest();
            
            _userRepoMock.Setup(x =>  x.IsUserExists(registerRequest.Email)).ReturnsAsync(true);


            var exception = Assert.ThrowsAsync<UserExistsException>(() => _userServiceMock.Register(registerRequest));

            exception.Result.Message.Should().Be("User already exists");

            

        }


        [Fact]
        public async Task RegisterUser_PostValidValue_ReturnOk()
        {
            var registerRequest = GenerateRegisterRequest();

            _userRepoMock.Setup(repo => repo.IsUserExists(registerRequest.Email)).ReturnsAsync(false);

            var result = _userServiceMock.Register(registerRequest);

            result.Result.Should().Be("Registration completed successfully");

        }


        private RegisterRequest GenerateRegisterRequest()
        {
            return new RegisterRequest
            {
                Name = "Artsiom",
                Lastname = "Kryvanos",
                Email = "test@gmail.com",
                Password = "password"
            };
        }


        private LoginRequest GenerateLoginRequest()
        {
            return new LoginRequest
            {
                Email = "test@gmail.com",
                Password = "password",
            };
        } 
        
        private LoginRequest GenerateWrongLoginRequest()
        {
            return new LoginRequest
            {
                Email = "test@gmail.com",
                Password = "password1",
            };
        }

        private User GenerateUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = "Artsiom",
                Lastname = "Kryvanos",
                Email = "test@gmail.com",
                Password = "$2a$10$i1NJOryJVvmCGyWfdUwjNOk7pCJoKJZc6nbMWzJBW.dzpvpTfR3Ly",
                ImageBase64 = "avatar",
            };
        }
       

        

    }


    
    
}


