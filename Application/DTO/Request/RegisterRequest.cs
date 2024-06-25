using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.DTO.Request;

public class RegisterRequest
{
    public required string Name { get; set; }

    public required string Lastname { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
