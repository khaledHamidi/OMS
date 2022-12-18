namespace OMS.Core.Services;

public interface ICacheService
{
    T GetData<T>(string key);
    void SetData<T>(string key, T value);
    void SetData<T>(string cacheKeys, T value, int absoluteExpirationRelativeToNow, int slidingExpiration, int size);
    void RemoveData(string key);
}

