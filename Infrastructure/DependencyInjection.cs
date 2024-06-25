using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<BlogAppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DB"));
        });

        //service.AddDbContextFactory<BlogAppDbContext>(options =>
        //{
        //    options.UseNpgsql(configuration.GetConnectionString("DB"));
        //});



        //service.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration = configuration.GetConnectionString("Redis");
        //    options.InstanceName = "BLOG_REDIS";
        //});

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ICommentRepository, CommentRepository>();
        service.AddScoped<IPostRepository, PostRepository>();

        return service;
    }
}