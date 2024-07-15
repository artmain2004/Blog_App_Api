using Application.DTO.Request;
using Application.DTO.Response;
using Domain.Entity;

namespace Application.Mapper;

public static class PostMapper
{
    public static PostDtoResponse ToPostDtoResponse(Post post)
    {
        return new PostDtoResponse()
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Name = post.User.Name,
            Lastname = post.User.Lastname,
            Email = post.User.Email,
            PostImage = post.PostImage
        };
    }
    
    public static SinglePostDtoResponse ToSinglePostDtoResponse(Post post)
    {
        return new SinglePostDtoResponse()
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Name = post.User.Name,
            Lastname = post.User.Lastname,
            Email = post.User.Email,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            PostImage = post.PostImage,
            Comments = post.Comments.Select(CommentMapper.ToCommentDto).ToList()
       
        };
    }

    public static Post GenerateNewPost(PostCreateRequest postCreateRequest, string imageString)
    {
        return new Post()
        {
            Id = Guid.NewGuid(),
            Title = postCreateRequest.Title,
            Body = postCreateRequest.Body,
            CreatedAt = DateTime.UtcNow,
            UserId = postCreateRequest.UserId,
            PostImage = imageString

        };


    }

    


}