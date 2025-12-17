using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }




        public async Task<string?> GetAsync(string key)
        {
           return await _cacheRepository.GetAsync(key);
        }



        public async Task SetAsync(string key, object value, TimeSpan time)
        {
            var CacheValueToJson = JsonSerializer.Serialize(value ,  new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            await _cacheRepository.SetAsync(key,CacheValueToJson, time);

        }
    }
}
