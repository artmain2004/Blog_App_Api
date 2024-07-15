using Application;
using Application.DTO.Request;
using Domain.Entity;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace blog_app_testing.Integration_Tests
{

    

    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _httpClient;

        private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;

      
        public UserControllerTests(CustomWebApplicationFactory<Program> customWebApplicationFactory)
        {

            _customWebApplicationFactory = customWebApplicationFactory;
            _httpClient = customWebApplicationFactory.CreateClient();
            
          
        }



       
        [Fact]
        public async Task Post_SendUserRegisterData_ReturnOk()
        {

            User registerRequest = new User()
            {
                Email = "qwerty@gmail.com",
                Password = "password",
                Name = "qwerty",
                Lastname = "qwerty"
            };


            var jsonRegister = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(registerRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/users/register", jsonRegister);

            var result = response.Content.ReadAsStringAsync();



            response.StatusCode.Should().Be(HttpStatusCode.OK);

            

        }



        [Fact]
        public async Task Post_SendUserLoginData_ReturnOk()
        {

            LoginRequest loginRequest = new()
            {
                Email = "email@gmail.com",
                Password = "password",
            };

            var jsonLogin = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/users/login", jsonLogin);             

            var result = response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);


        }

       

    }
}
