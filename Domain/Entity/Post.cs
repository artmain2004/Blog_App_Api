namespace Domain.Entity;

public class Post
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Body { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public required Guid UserId { get; set; }

    public User User { get; set; }

    public List<Comment> Comments { get; set; }


}

    