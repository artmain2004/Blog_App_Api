using Application.DTO.Request;
using Application.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IPostService
    {
        Task<List<PostDtoResponse>> GetAllPosts();
        Task<SinglePostDtoResponse?> GetPostById(Guid id);
        Task<string> CreatePost(PostCreateRequest postCreateRequest);
        Task<string> DeletePost(Guid id);
        Task<string> UpdatePost(PostUpdateRequest postUpdateRequest, Guid postId);
        Task<List<PostDtoResponse>> GetPostsByTitle(string title);

        Task<List<PostDtoResponse>> GetPostsById(Guid id);
    }
}
