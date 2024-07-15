using Application.DTO.Request;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace blog_app_testing.Integration_Tests
{
    public class CommentControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;


        public CommentControllerTests(CustomWebApplicationFactory<Program> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
            _httpClient = customWebApplicationFactory.CreateClient();
        }


        [Fact]
        public async Task CreateNewComment_PostComment_ReturnSuccessMessage()
        {

            var userId = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192c");
            var postId = Guid.Parse("c36aaa54-5255-4519-ae2b-f249867c192c");

            CreateCommentRequest commentRequest = new CreateCommentRequest { Body = "fisrt comment" };




            var commentJson = new StringContent(
                JsonSerializer.Serialize(commentRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync($"/api/comments?userId={userId}&postId={postId}", commentJson);

            var result = response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

    }
}
