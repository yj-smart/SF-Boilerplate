

using Microsoft.Extensions.Caching.Memory;

namespace SF.Web.Components
{
    public class CacheHelper
    {
        public CacheHelper(
            IMemoryCache cache
            )
        {
            this.cache = cache;
        }

        private IMemoryCache cache;

        public void ClearCache(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
    }
}
