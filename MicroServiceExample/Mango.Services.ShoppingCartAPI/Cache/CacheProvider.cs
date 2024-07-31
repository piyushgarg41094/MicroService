using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace Mango.Services.ShoppingCartAPI.Cache
{
    public class CacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConnectionMultiplexer _redisCache;
        public const long One_Year_Seconds = 31536000;
        public CacheProvider(IMemoryCache memoryCache, IConnectionMultiplexer redisCache)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        public T Get<T>(string key, CacheFrom cacheFrom = CacheFrom.Memory)
        {
            if (cacheFrom == CacheFrom.Memory)
            {
                return _memoryCache.Get<T>(key);
            }
            else
            {
                var db = _redisCache.GetDatabase();
                return ByteArrayToObject<T>(db.StringGet(key));
            }
        }

        public void Remove(string key, CacheFrom cacheFrom = CacheFrom.Memory)
        {
            if (cacheFrom == CacheFrom.Memory)
            {
                _memoryCache.Remove(key);
            }
            else
            {
                var db = _redisCache.GetDatabase();
                db.KeyDelete(key);
            }
        }

        public void Set(string key, object value, long? expiration = null, CacheFrom cacheFrom = CacheFrom.Memory)
        {
            if (cacheFrom == CacheFrom.Memory)
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

                if (expiration.HasValue && expiration > 0)
                {
                    options.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(expiration.Value);
                }
                else
                {
                    options.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(One_Year_Seconds);
                }

                _memoryCache.Set(key, value, options);
            }
            else
            {
                var db = _redisCache.GetDatabase();
                db.StringSet(key, ObjectToByteArray(value), TimeSpan.FromSeconds(One_Year_Seconds));
            }

        }

        private T ByteArrayToObject<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            string jsonFromBytes = Encoding.UTF8.GetString(data);
            object obj = JsonConvert.DeserializeObject(jsonFromBytes);
            return (T)obj;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var ans = JsonConvert.SerializeObject(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(ans);
            return byteArray;
        }
    }
}
