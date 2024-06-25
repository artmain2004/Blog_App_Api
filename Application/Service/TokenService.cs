
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class TokenService : ITokenService
{
	
	private readonly IConfiguration _configuration;
	
	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}
		public string GenerateJwtToken(Guid id, string name)
		{
			
			var handler = new JwtSecurityTokenHandler();
			
			var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtOptions:Key").Value) ;

			
			
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
			
			var claims = new ClaimsIdentity(new []
			{
				new Claim ("Id", id.ToString()),
				new Claim  ("Name", name)
			});
			
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claims,
				SigningCredentials = signingCredentials,
				Expires =  DateTime.UtcNow.AddMinutes(1)
			};
			
			var token = handler.CreateToken(tokenDescriptor);
			
			
			return handler.WriteToken(token);
		}
}
