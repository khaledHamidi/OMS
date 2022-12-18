namespace OMS.Common.Configurations.Caching;

public class MemoryCacheConfig
{
    public int AbsoluteExpirationRelativeToNow { get; set; }
    public int SlidingExpiration { get; set; }
    public int Size { get; set; }
}
