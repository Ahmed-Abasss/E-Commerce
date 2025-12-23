using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerce.Presentation.Controllers
{
    
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

      

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        //Get All Product
        [HttpGet]
        [RedisCache]

        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            
            var Products =await _productService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }



        //Get Product By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {

            var result = await _productService.GetProductByIdAsync(id);
            return ResultHandler<ProductDto>(result);
        }

        //Get Brands

        [HttpGet("types")]

        public async Task<ActionResult<TypeDto>> GetProductsTypes()
        {
            var PTypes = await _productService.GetAllTypesAsync();
            return Ok(PTypes);
        }

        //Get Types

        [HttpGet("brands")]

        public async Task<ActionResult<BrandDto>> GetProductsBrands()
        {
            var PBrands = await _productService.GetAllBrandsAsync();
            return Ok(PBrands);
        }


    }
}
