using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey)
        {
            var CacheValue =  await _database.StringGetAsync(cacheKey);

            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan timeToLive)
        {
             await _database.StringSetAsync(cacheKey, value, timeToLive);
        }
    }
}
