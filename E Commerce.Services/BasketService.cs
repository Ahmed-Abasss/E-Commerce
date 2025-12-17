using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);

            return _mapper.Map<BasketDto>(CreateOrUpdateBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)=> await _basketRepository.deleteBasketAsync(basketId);

        public async Task<BasketDto> GetBasketAsync(string basketId)
        {
          var Basket= await  _basketRepository.GetBasketAsync(basketId);
            if (Basket is null)
                throw new BasketNotFoundException(basketId);
            return _mapper.Map<BasketDto>(Basket);
        }
    }
}
