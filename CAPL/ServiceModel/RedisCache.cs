

namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using StackExchange.Redis;
    using System;

    public class RedisCache : ICaplCache
    {
        public RedisCache()
        {            
        }

        public RedisCache(int databaseNo, string connectionString, TimeSpan ttl)
        {
            this.DatabaseNo = databaseNo;
            this.ConnectionString = connectionString;
            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            connection = ConnectionMultiplexer.Connect(options);
            database = connection.GetDatabase(databaseNo);
            DefaultTTL = ttl;
        }

        public int DatabaseNo { get; set; }
        public string ConnectionString { get; set; }

        public TimeSpan DefaultTTL { get; set; }

        private ConnectionMultiplexer connection;
        private IDatabase database;
        public AuthorizationPolicy Get(string key)
        {
            return database.Get<AuthorizationPolicy>(key);
        }

        public void Set(string key, AuthorizationPolicy policy, TimeSpan ttl)
        {
            database.Set(key, policy);
            database.KeyExpire(key, ttl);
        }

        public void Remove(string key)
        {
            database.KeyDelete(key);
        }

        public void Set(string key, AuthorizationPolicy policy)
        {
            database.Set(key, policy);
            database.KeyExpire(key, DefaultTTL);
        }
    }
}
