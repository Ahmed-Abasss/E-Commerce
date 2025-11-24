using E_Commerce.Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> CreateOrUpdateBasketAsync (CustomerBasket basket , TimeSpan timeToLive = default);

        Task<bool> deleteBasketAsync (string basketId);


        Task<CustomerBasket?> GetBasketAsync (string basketId);
    }
}
