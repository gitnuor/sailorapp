using BDO.Core.DataAccessObjects.ExtendedEntities;
using EPYSLSAILORAPP.Application.Interface.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ServiceStack.Redis;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class RedisService<T> : IRedisService<T> where T : class
    {
        private readonly IConfiguration _configuration;
        //public string
        public string Host = "localhost";

        

        public RedisService(IConfiguration configuration)
        {
            _configuration = configuration;
            var obj = _configuration.GetSection(nameof(RedisConnectionStrings)).Get<RedisConnectionStrings>();
            Host = obj.redisServerUrl;
        }
        public List<T> GetDataFromRedisCache(string key)
        {
            try
            {
                using (RedisClient client = new RedisClient(Host))
                {
                    return client.Get<List<T>>(key);

                }
            }
            catch(Exception es)
            {
                throw es;
            }
        }

        public void SaveDataInRedisCache(string key, Object value)
        {
            try
            {
                using (RedisClient client = new RedisClient(Host))
                {
                    client.Set(key, value, DateTime.UtcNow.AddMinutes(3600));
                }
            }
            catch (Exception es)
            {
                throw es;
            }
           
        }
        public void SaveDataInRedisCache(string key, Object value, int timeoutInMinute)
        {
            try
            {
                using (RedisClient client = new RedisClient(Host))
                {
                    if (client.Get<string>(key) == null)
                    {
                        client.Set(key, value, DateTime.UtcNow.AddMinutes(timeoutInMinute));

                    }
                }
            }
            catch (Exception es)
            {
                throw es;
            }
           
        }

        public bool RemoveKey(string key)
        {
         
            try
            {
                using (var client = new RedisClient(Host))
                {
                    if (client.ContainsKey(key))
                    {
                        client.Remove(key);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public void ClearAllKeys()
        {
            try
            {
                using (var client = new RedisClient(Host))
                {
                    client.FlushAll();
                }
            }
            catch (Exception es)
            {
                throw es;
            }
           
          
        }
        public bool IsKeyExists(string key)
        {
            try
            {
                using (var client = new RedisClient(Host))
                {
                    if (client.ContainsKey(key))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception es)
            {
                throw es;
            }
           
        }
    }
}
