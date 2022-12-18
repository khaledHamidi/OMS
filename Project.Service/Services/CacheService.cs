using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OMS.Common.Configurations.Caching;
using OMS.Core.Services;

namespace OMS.Service.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<MemoryCacheConfig> _memoryCacheConfigOption;

    public CacheService(IMemoryCache memoryCache,
        IOptions<MemoryCacheConfig> memoryCacheConfigOption)
    {
        _memoryCacheConfigOption = memoryCacheConfigOption;
        _memoryCache = memoryCache;
    }

    public T GetData<T>(string key)
    {
        T item = (T)_memoryCache.Get(key);
        return item;
    }

    public void SetData<T>(string cacheKeys, T value)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(_memoryCacheConfigOption.Value.AbsoluteExpirationRelativeToNow))
            .SetSlidingExpiration(TimeSpan.FromSeconds(_memoryCacheConfigOption.Value.SlidingExpiration))
            .SetSize(_memoryCacheConfigOption.Value.Size);

        _memoryCache.Set(cacheKeys, value, cacheEntryOptions);
    }

    public void SetData<T>(string cacheKeys, T value, int absoluteExpirationRelativeToNow, int slidingExpiration, int size)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationRelativeToNow))
            .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpiration))
            .SetSize(size);

        _memoryCache.Set(cacheKeys, value, cacheEntryOptions);
    }

    public void RemoveData(string cacheKeys)
    {
        if (!string.IsNullOrEmpty(cacheKeys))
        {
            _memoryCache.Remove(cacheKeys);
        }
    }
}

