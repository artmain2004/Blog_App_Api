using Domain.Entity;

namespace Domain.Interface.Redis;

public interface IRedis<T>  where T : class
{
    Task SetToCache(string key, Post data);

    Task<T?> GetFromCache(string key);
}