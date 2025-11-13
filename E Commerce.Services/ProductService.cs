using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var AllBrands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

           return _mapper.Map<IEnumerable<BrandDto>>(AllBrands);
           
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var AllProducts = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(AllProducts);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var AllTypes = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(AllTypes);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
           var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(Product);
        }
    }
}
