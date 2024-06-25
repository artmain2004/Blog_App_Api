using Application.Interface;
using Application.Mapper;
using Domain.Interface;
using Application.Exceptions.UserExceptions;
using Application.Exceptions.PostExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repository;
using Application.DTO.Request;

namespace Application.Service
{
    public class CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository) : ICommentService
    {

        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<string> CreateComment(CreateCommentRequest createComment, Guid userId, Guid postId)
        {
            var userById = await _userRepository.GetUserById(userId);

            var postById = await _postRepository.GetPostById(postId);

            if (userById is null) throw new UserNotFoundException("User not found");

            if (postById is null) throw new PostNotFoundException("Post not found");

            var newComment = CommentMapper.GenerateNewComment(createComment, userById, postById.Id);

            await _commentRepository.CreateComment(newComment);

            return "Сomment was added successfully";
        }
    }
}
