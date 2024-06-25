namespace Domain.Entity;

public class User
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Lastname { get; set; }

	public required string Email { get; set; }

	public required string Password { get; set; }
	
	
	public static User GenerateNewUser(string name, string lastname, string email, string password)
	{
		return new User()
		{
			Id = Guid.NewGuid(),
			Name = name,
			Lastname = lastname,
			Email = email,
			Password = password
		};
	}
}