namespace Domain.Entity;

public class Comment
{
    public Guid Id { get; set; }

    public required string Body { get; set; }

    public required string Name { get; set; }

    public required string Lastname { get; set; }

    public required string Email { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required Guid PostId { get; set; }

    public Post Post { get; set; }
}