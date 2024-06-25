namespace Application;

public interface ITokenService
{
  string GenerateJwtToken(Guid id, string name);
}
