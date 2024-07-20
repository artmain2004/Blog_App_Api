using System.Text;
using Application.Exceptions;
using Application.Exceptions.PostExceptions;
using Application.Interface;
using Application.Service;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
	{
		
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
		{
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtOptions:Key").Value))
			};
			
			
			
			
		});
		
		
		services.AddAuthorization();
		
		
		services.AddScoped<IPostService, PostService>();
		
		services.AddScoped<IUserService,  UserService>();
		
		services.AddScoped<ITokenService,  TokenService>();

		services.AddScoped<ICommentService, CommentService>();

        services.AddExceptionHandler<GlobalExceptions>();

        

        

		
		return services;
	}
}
