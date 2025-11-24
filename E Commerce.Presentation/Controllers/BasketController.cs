using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]

        public async Task<ActionResult<BasketDto>> GetBasketById(string id)
        {
            var BasketItem=await _basketService.GetBasketAsync(id);
            return Ok(BasketItem);
        }

        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var CreateOrUpdateBasket =await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(CreateOrUpdateBasket);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var Result = await _basketService.DeleteBasketAsync(id);
            return Ok(Result); 
        }


    }
}
