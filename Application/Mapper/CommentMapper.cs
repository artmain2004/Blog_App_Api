using Application.DTO;
using Application.DTO.Request;
using Domain.Entity;

namespace Application.Mapper;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(Comment comment)
    {
        return new CommentDto()
        {
            Id = comment.Id,
            Body = comment.Body,
            Name = comment.Name,
            Lastname = comment.Lastname,
            Email = comment.Email,
            CreatedAt = comment.CreatedAt
        };
    }


    public static Comment GenerateNewComment(CreateCommentRequest commentRequest, User user, Guid postId)
    {
        return new Comment()
        {
            Id = Guid.NewGuid(),
            Body = commentRequest.Body,
            PostId = postId,
            Name = user.Name,
            Lastname = user.Lastname,
            Email = user.Email,
            CreatedAt = DateTime.UtcNow
        };
    }
}