using Application.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICommentService
    {
        Task<string> CreateComment(CreateCommentRequest createComment, Guid userId, Guid postId);
    }
}
