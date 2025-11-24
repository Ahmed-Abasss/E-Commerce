using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product , int>
    {

        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams) 
            :base(ProductSpecificationsHelper.GetProductCriteria(queryParams))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch(queryParams.Sort)
            {
                case ProductOrderByType.NameAsce:
                    AddOrderBy(p=>p.Name); 
                    break;

                case ProductOrderByType.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductOrderByType.PriceAsce:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductOrderByType.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;

            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }
        public ProductWithBrandAndTypeSpecification(int id ):base(p=>p.Id==id) 
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        // for getting all Products
        public ProductWithBrandAndTypeSpecification():base(null)
        {
            AddInclude(p=>p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
