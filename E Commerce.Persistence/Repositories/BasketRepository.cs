using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
          var  basketjson = JsonSerializer.Serialize(basket);

           var IsCreatedOrUpdatedBasket = await _database.StringSetAsync(basket.Id, basketjson, (timeToLive == default) ? TimeSpan.FromDays(7) : timeToLive);

            if(IsCreatedOrUpdatedBasket)
            {
                return await GetBasketAsync(basket.Id);
            }
            else 
                return null;

            
        } 

        public async Task<bool> deleteBasketAsync(string basketId)=>await  _database.KeyDeleteAsync(basketId);
        

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        { 
            var Basket = await  _database.StringGetAsync(basketId);
            
            if(!string.IsNullOrEmpty(Basket))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }
            else
            {
                return null; 
            }
        }
    }
}
