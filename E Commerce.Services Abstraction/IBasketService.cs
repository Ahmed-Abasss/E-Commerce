using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IBasketService
    {
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);

        Task<bool> DeleteBasketAsync(string basketId);

        Task<BasketDto> GetBasketAsync(string basketId);
    }
}
