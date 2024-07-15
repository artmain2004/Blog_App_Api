using Domain.Entity;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PostRepository(BlogAppDbContext context) : IPostRepository
{
	private readonly BlogAppDbContext _context = context;


	public async Task<List<Post>> GetAllPosts()
	{
		return await _context.Posts
			.Include(u => u.User)   
			.ToListAsync();
	}
	public async Task<List<Post>> GetAllPostsByTitle(string title)
	{
		return await _context.Posts
			.Where(post => post.Title.Contains(title))
			.Include(u => u.User)
			.ToListAsync();
	}

	public async Task<List<Post>> GetAllPostsByUserId(Guid id)
	{
		return await _context.Posts
			.Where(p => p.UserId == id)
			.Include(u => u.User)
			.ToListAsync();
	}

	public async Task<Post?> GetPostById(Guid id)
	{
		return await _context.Posts
			.Include(p => p.Comments)
			.Include(p => p.User)
			.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task CreatePost(Post post)
	{
		await _context.Posts.AddAsync(post);

		await _context.SaveChangesAsync();
		
		
	}

	public async Task UpdatePost( Guid id, string title, string body )
	{
		await _context.Posts
		   .Where(p => p.Id == id)
		   .ExecuteUpdateAsync(s => s
				.SetProperty(p => p.Title, title)
				.SetProperty( p => p.Body, body)
				.SetProperty (p => p.UpdatedAt, DateTime.UtcNow)
		   );
			

		
		
		

		await _context.SaveChangesAsync();
		
		
	
	}

	public async Task DeletePost(Post post)
	{
        _context.Posts.Remove(post);

		await _context.SaveChangesAsync();
	}
	


}

