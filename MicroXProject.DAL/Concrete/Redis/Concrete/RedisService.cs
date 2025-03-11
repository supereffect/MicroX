using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.DAL.Concrete.Redis.Concrete
{

    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task SetValueAsync(string key, string value)
        {
            try
            {
                var db = _connectionMultiplexer.GetDatabase();
                await db.StringSetAsync(key, value);
            }
            catch (RedisConnectionException ex)
            {
                // Bağlantı hatası meydana geldiğinde daha fazla bilgi alın
                Console.WriteLine($"Redis connection error: {ex.Message}");
                throw ex;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw e;
            }
        }

        public async Task<string> GetValueAsync(string key)
        {
            try
            {
                var db = _connectionMultiplexer.GetDatabase();
                return await db.StringGetAsync(key);
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine($"Redis connection error: {ex.Message}");
                throw ex;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw e;
            }
        }
    }
}
