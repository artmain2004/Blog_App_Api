namespace Application.DTO;

public class CommentDto
{
    public Guid Id { get; set; }

    public  string Body { get; set; }

    public  string Name { get; set; }

    public  string Lastname { get; set; }

    public  string Email { get; set; }
    
    public required DateTime CreatedAt { get; set; }
}