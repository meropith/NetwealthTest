namespace Exchange.API.Models
{
    public interface ICacheable
    {
        string CacheKey { get; }     
        string UserTier { get; }
    }
}
