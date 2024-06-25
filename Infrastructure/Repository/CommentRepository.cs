using Domain.Entity;
using Domain.Interface;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class CommentRepository(BlogAppDbContext context) : ICommentRepository
{
    private readonly BlogAppDbContext _context = context;

    public async Task CreateComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    
}