using Application.DTO.Request;
using Application.DTO.Response;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace blog_app_testing.Integration_Tests
{
    public class PostControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;


        public PostControllerTests(CustomWebApplicationFactory<Program> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
            _httpClient = customWebApplicationFactory.CreateClient();
        }

        


        [Fact]

        public async Task GetAllPosts_ReturnOk()
        {
            var response = await _httpClient.GetAsync("/api/posts");

            var result = response.Content.ReadAsStringAsync();


            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]

        public async Task GetPostById_ReturnPost()
        {
            var postId = Guid.Parse("c36aaa54-5255-4519-ae2b-f249867c192c");
            

            var response = await _httpClient.GetAsync($"api/posts/{postId}");

            var result = response.Content.ReadAsStringAsync();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
        }

        [Fact]

        public async Task GetPostsByTitle_ReturnPostsByTitle()
        {
            var postTitle = "post title 1";


            var response = await _httpClient.GetAsync($"api/posts/title?title={postTitle}");

            var result = response.Content.ReadAsStringAsync();

           

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
        }


        [Fact]

        public async Task DeletePostById_ReturnOk()
        {
            var postId = Guid.Parse("c37aaa54-5255-4519-ae2b-f249867c192c");

            var response = await _httpClient.DeleteAsync($"api/posts/{postId}");


            var result = response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        //[Fact]
        //public async Task CreateNewPost_ReturnOk()
        //{
        //    var formData = new PostCreateRequest
        //    {
        //        UserId = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192c"),
        //        Title = "post title",
        //        Body = "post body"
        //    };

        //    var formDictionary = new Dictionary<string, string>
        //    {
        //        {"UserId", formData.UserId.ToString()  },
        //        {"Title", formData.Title },
        //        {"Body", formData.Body}
        //    };

        //    var content = new FormUrlEncodedContent(formDictionary);

        //    var response = await _httpClient.PostAsync("api/posts", content);

        //    var result = response.Content.ReadAsStringAsync();

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

    }
}
