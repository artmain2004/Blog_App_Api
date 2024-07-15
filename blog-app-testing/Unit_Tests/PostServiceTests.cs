using Application.Interface;
using Application.Mapper;
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
    public class PostServiceTests
    {

        private readonly IPostService _postService;
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public PostServiceTests()
        {
            _postRepositoryMock = new Mock<IPostRepository>();

            _postService = new PostService(_postRepositoryMock.Object);

        }

        [Fact]
        public async Task GetAllPosts_ReturnAllPosts()
        {
            var posts = new List<Post>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Title = "post title 1",
                    Body = "post body 1",
                    CreatedAt = DateTime.Now,
                    UserId = Guid.NewGuid(),

                },new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "post title 2",
                    Body = "post body 2",
                    CreatedAt = DateTime.Now,
                    UserId = Guid.NewGuid(),

                },new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "post title 3",
                    Body = "post body 3",
                    CreatedAt = DateTime.Now,
                    UserId = Guid.NewGuid(),

                }
            };


             _postRepositoryMock.Setup(repo => repo.GetAllPosts()).ReturnsAsync(posts);

            

            var result = await _postService.GetAllPosts();


            Assert.Equal(3, result.Count);
        }

    }
}
