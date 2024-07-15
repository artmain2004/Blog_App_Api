using Application.DTO.Request;
using Application.Exceptions.PostExceptions;
using Application.Exceptions.UserExceptions;
using Application.Interface;
using Application.Service;
using Domain.Entity;
using Domain.Interface;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_app_testing.Unit_Tests
{
    public class CommentServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPostRepository> _postRepositoryMock;
        private readonly Mock<ICommentRepository> _commentRepositoryMock;
        private readonly ICommentService _commentServiceMock;

        public CommentServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _postRepositoryMock = new Mock<IPostRepository>();
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _commentServiceMock = new CommentService(_commentRepositoryMock.Object, _userRepositoryMock.Object, _postRepositoryMock.Object );
        }


        [Fact]
        public async Task CreateNewComment_PostCorrectData_ReturnSuccessMessage()
        {
            var newCommentRequest = GenerateCommentRequest();

            var userById = GenerateUser();

            var postById = GeneratePost(userById.Id);

            var newComment = GenerateComment(userById, postById.Id);

            _userRepositoryMock.Setup(userRepo => userRepo.GetUserById(userById.Id)).ReturnsAsync(userById);

            _postRepositoryMock.Setup(postRepo => postRepo.GetPostById(postById.Id)).ReturnsAsync(postById);

            _commentRepositoryMock.Setup(commentRepo => commentRepo.CreateComment(newComment)).Returns(Task.CompletedTask);

            
            var result = _commentServiceMock.CreateComment(newCommentRequest, userById.Id, postById.Id);


            result.Result.Should().Be("Сomment was added successfully");
        }
         [Fact]
        public async Task CreateNewComment_PostIncorrectUserId_ReturnUserNotFoundException()
        {
            var newCommentRequest = GenerateCommentRequest();

            var userById = GenerateUser();

            var postById = GeneratePost(userById.Id);

            

            _userRepositoryMock.Setup(userRepo => userRepo.GetUserById(userById.Id)).Returns(Task.FromResult<User>(null));

            var exception = Assert.ThrowsAsync<UserNotFoundException>(() => _commentServiceMock.CreateComment(newCommentRequest, userById.Id, postById.Id));



            exception.Result.Message.Should().Be("User not found");
        }
         [Fact]
        public async Task CreateNewComment_PostIncorrectPostId_ReturnPostNotFoundException()
        {
            var newCommentRequest = GenerateCommentRequest();

            var userById = GenerateUser();

            var postById = GeneratePost(userById.Id);

            var newComment = GenerateComment(userById, postById.Id);

            _userRepositoryMock.Setup(userRepo => userRepo.GetUserById(userById.Id)).ReturnsAsync(userById);

            _postRepositoryMock.Setup(postRepo => postRepo.GetPostById(postById.Id)).Returns(Task.FromResult<Post>(null));


            var exception = Assert.ThrowsAsync<PostNotFoundException>(() => _commentServiceMock.CreateComment(newCommentRequest, userById.Id, postById.Id));


            exception.Result.Message.Should().Be("Post not found");

        }


        private User GenerateUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Name = "Artsiom",
                Lastname = "Kryvanos",
                Email = "test@gmail.com",
                Password = "$2a$10$i1NJOryJVvmCGyWfdUwjNOk7pCJoKJZc6nbMWzJBW.dzpvpTfR3Ly",
                ImageBase64 = "avatar",
            };
        }

        private Post GeneratePost(Guid id)
        {
            return new Post()
            {
                Id= Guid.NewGuid(),
                Title = "title",
                Body = "body",
                UserId = id,
                CreatedAt = DateTime.UtcNow,
            };
        }

        private CreateCommentRequest GenerateCommentRequest()
        {
            return new CreateCommentRequest()
            {
                Body = "comment body",
                
            };
                
         }

        private Comment GenerateComment(User user, Guid postId)
        {
            return new Comment()
            {
                Body = "comment body",
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                CreatedAt = DateTime.UtcNow,
                PostId = postId
            };
        }

    }
}
