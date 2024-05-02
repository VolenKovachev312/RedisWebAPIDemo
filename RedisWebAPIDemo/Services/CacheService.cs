using StackExchange.Redis;
using System.Text.Json;

namespace RedisWebAPIDemo.Services;

public class CacheService : ICacheService
{
    private IDatabase _cacheDb;

    public CacheService()
    {
        var redist = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = redist.GetDatabase();
    }
    public T? GetData<T>(string key)
    {
        RedisValue value = _cacheDb.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value!);
        }

        return default;
    }

    public bool RemoveData(string key)
    {
        var dataExists = _cacheDb.KeyExists(key);

        if (dataExists)
        {
            return _cacheDb.KeyDelete(key);
        }

        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
    }
}
