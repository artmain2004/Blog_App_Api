using Domain.Entity;

namespace Domain.Interface;

public interface ICommentRepository
{
    Task CreateComment(Comment comment);
 
}