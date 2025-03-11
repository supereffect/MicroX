using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;
using StackExchange.Redis;

namespace CQRSWebApiProject.DAL.Concrete.Redis.Concrete
{
    public class CacheService : ICacheService, IDisposable
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;

        // Constructor with connection and database initialization
        public CacheService()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "172.20.0.2:6379", "172.20.0.3:6379" }, // Redis Cluster node'lar
                AllowAdmin = true,
                AbortOnConnectFail = false // Bağlantı hatası durumunda programın çökmesini engelle
            };

            _connection = ConnectionMultiplexer.Connect(configurationOptions);
            _database = _connection.GetDatabase();
        }

        // Veri setleme
        public void SetCache(string key, string value, TimeSpan? expiry = null)
        {
            try
            {
                if (expiry.HasValue)
                {
                    _database.StringSet(key, value, expiry.Value);
                }
                else
                {
                    _database.StringSet(key, value);
                }
            }
            catch (Exception e)
            {
                string test = " ";
            }
        }


        // Veri okuma
        public string GetCache(string key)
        {
            return _database.StringGet(key);
        }

        // Bağlantıyı düzgün şekilde kapatma
        public void Dispose()
        {
            if (_connection != null && _connection.IsConnected)
            {
                _connection.Dispose();
            }
        }

    }
}