using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
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

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecification(queryParams);
            var AllProducts = await repo.GetAllAsync(Specifications);

            var MappedData= _mapper.Map<IEnumerable<ProductDto>>(AllProducts);
            
            var countPaginatedData = MappedData.Count();

            var specForCount = new ProductCountSpecification(queryParams);

            var countAllData =await repo.CountAllAsync(specForCount);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex , countPaginatedData, countAllData, MappedData);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var AllTypes = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(AllTypes);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
           var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(spec);
            if (Product is null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<ProductDto>(Product);
        }
    }
}
