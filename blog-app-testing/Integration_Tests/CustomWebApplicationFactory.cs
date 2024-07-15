using Application;
using Application.Exceptions;
using Application.Interface;
using Application.Service;
using Domain.Entity;
using Domain.Interface;
using FluentAssertions.Common;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_app_testing.Integration_Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(service =>
            {
               
                //var dbContext = service.SingleOrDefault(s => s.ServiceType == typeof(BlogAppDbContext));
                var dbContextDescriptor = service.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<BlogAppDbContext>));
                

                
                //service.Remove(dbContext);
                service.Remove(dbContextDescriptor);

                service.AddDbContext<BlogAppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("blog-app");
                });


                service.AddAuthentication("TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

                var sp = service.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BlogAppDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<Program>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        //SeedDatabasePosts(db);
                        // Seed the database with test data if necessary
                        SeedDatabase(db);
                        
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }

                

        


        

                service.AddScoped<IUserService, UserService>();
                service.AddScoped<IUserRepository, UserRepository>();
                service.AddExceptionHandler<GlobalExceptions>();
                service.AddScoped<ITokenService, TokenService>();
                service.AddScoped<IPostRepository, PostRepository>();
                service.AddScoped<IPostService, PostService>();
            });
        }

        


        private void SeedDatabase(BlogAppDbContext context)
        {
            List<User> users = new()
            {
                new User()
                {
                    Id = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192c") ,
                    Name = "Artsiom",
                    Lastname = "Kryvanos",
                    Email = "email@gmail.com",
                    Password = "$2a$10$i1NJOryJVvmCGyWfdUwjNOk7pCJoKJZc6nbMWzJBW.dzpvpTfR3Ly",
                    ImageBase64 = "string"
                }, new User()
                {
                    Id = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192d"),
                    Name = "Lera",
                    Lastname = "Kryvanos",
                    Email = "email1@gmail.com",
                    Password = "password1",
                    ImageBase64 = "string"
                },
            };

            List<Post> posts = new()
            {
                new Post()
                {
                    Id = Guid.Parse("c36aaa54-5255-4519-ae2b-f249867c192c"),
                    Title = "post title 1",
                    Body = "post body 1",
                    UserId = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192c"),
                    CreatedAt = DateTime.UtcNow,
                    PostImage = "post image 1"

                },

                new Post()
                {
                    Id = Guid.Parse("c37aaa54-5255-4519-ae2b-f249867c192c"),
                    Title = "post title 2",
                    Body = "post body 2",
                    UserId = Guid.Parse("c35aaa54-5255-4519-ae2b-f249867c192d"),
                    CreatedAt = DateTime.UtcNow,
                    PostImage = "post image 2"
                }
            };
            context.Users.AddRange(users);

            context.Posts.AddRange(posts);




          
             context.SaveChanges();

               
        }
    }
}
