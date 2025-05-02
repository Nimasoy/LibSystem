using Microsoft.Extensions.Caching.Memory;
using Library.Application.Interfaces; // Using the interface from the Application layer

namespace Library.Infrastructure.Services;

// Removed duplicate interface definition as it's now in Library.Application.Interfaces

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _defaultOptions;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
        _defaultOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));
    }

    public T? Get<T>(string key)
    {
        return _cache.TryGetValue(key, out T? value) ? value : default;
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = expiration.HasValue
            ? new MemoryCacheEntryOptions().SetSlidingExpiration(expiration.Value)
            : _defaultOptions;

        _cache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
} 