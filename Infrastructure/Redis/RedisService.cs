using System.Text.Json;
using Domain.Entity;
using Domain.Interface.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Redis;

public class RedisService(IDistributedCache cache) : IRedis<Post>
{

    private readonly IDistributedCache _cache = cache;


    public async Task SetToCache(string key, Post data)
    {
        var postKey = $"post_{key}";

        var postData = JsonSerializer.Serialize(data);

        await _cache.SetStringAsync(postKey, postData);
        
    }

    public async Task<Post?> GetFromCache(string key)
    {
        var postKey = $"post_{key}";

        var data = await _cache.GetStringAsync(postKey);

        if (string.IsNullOrEmpty(data))
        {
            return null;
        }

        var postData = JsonSerializer.Deserialize<Post>(data);


        return postData;
    }
}