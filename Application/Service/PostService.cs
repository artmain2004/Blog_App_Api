using Application.Mapper;
using Domain.Entity;
using Domain.Interface;
using Domain.Interface.Redis;
using Application.Interface;
using Application.Exceptions.PostExceptions;
using Application.DTO.Request;
using Application.DTO.Response;
using Microsoft.AspNetCore.Http;


namespace Application.Service;

public class PostService(IPostRepository postRepository) : IPostService
{
	public async Task<List<PostDtoResponse>> GetAllPosts()
	{
		var post = await postRepository.GetAllPosts();

		var postDto = post.Select(PostMapper.ToPostDtoResponse).ToList();

		return postDto;
	}

	public async Task<SinglePostDtoResponse?> GetPostById(Guid id)
	{
		var postFromCache = await redis.GetFromCache(id.ToString());

		if (postFromCache is not null)
		{
			var postFromCacheDto = PostMapper.ToSinglePostDtoResponse(postFromCache);
			return postFromCacheDto;
		}

		

		var postFromDb = await postRepository.GetPostById(id);

		

		if (postFromDb is null) throw new PostNotFoundException("Post not found") ;
        

        	var postFromDbDto = PostMapper.ToSinglePostDtoResponse(postFromDb);

		

		await redis.SetToCache(id.ToString(), postFromDb);
		
		return postFromDbDto ;
	}
	

	public async Task<string> CreatePost(PostCreateRequest postCreateRequest)
	{

		var imageBytes = ImageMapper.ImageToByteArray(postCreateRequest.PostImage);

		var imageString = Convert.ToBase64String(imageBytes);

		var newPost = PostMapper.GenerateNewPost(postCreateRequest, imageString);

		await postRepository.CreatePost(newPost);
		
		return "Post was created successfully";
	}

	public async Task<string> DeletePost(Guid id)
	{

		var postById = await postRepository.GetPostById(id);

		if (postById is null) throw new PostNotFoundException("Post not found");

		await postRepository.DeletePost(postById);
		
		return "Post was deleted successfully";
	}

	public async Task<string> UpdatePost(PostUpdateRequest postUpdateRequest, Guid postId)
	{
		var postById = await postRepository.GetPostById(postId);

		if (postById is null) throw new PostNotFoundException("Post not found");

        

		await postRepository.UpdatePost(postId, postUpdateRequest.Title, postUpdateRequest.Body);
		
		return "Post was updated successfully";
	}

	public async Task<List<PostDtoResponse>> GetPostsByTitle(string title)
	{
		
		var posts = await postRepository.GetAllPostsByTitle(title);


		var postsDto = posts.Select(PostMapper.ToPostDtoResponse).ToList();

		return postsDto;
	}

	public async Task<List<PostDtoResponse>> GetPostsById(Guid id)
	{
		var posts = await postRepository.GetAllPostsByUserId(id);

		var postsDto = posts.Select(PostMapper.ToPostDtoResponse).ToList();

		return postsDto;
	}

   
}
