using Domain.Entity;

namespace Domain.Interface;

public interface IPostRepository
{
    Task<List<Post>> GetAllPosts();
    Task<List<Post>> GetAllPostsByTitle(string title);
    
    Task<List<Post>> GetAllPostsByUserId(Guid id);
    Task<Post?> GetPostById(Guid id);
    Task CreatePost(Post post);
    Task UpdatePost(Guid id, string title, string body);
    Task DeletePost(Post post);
}