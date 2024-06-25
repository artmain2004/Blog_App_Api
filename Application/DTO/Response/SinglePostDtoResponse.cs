namespace Application.DTO.Response;

public class SinglePostDtoResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public string Lastname { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<CommentDto> Comments { get; set; }
}