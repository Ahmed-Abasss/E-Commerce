using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;

            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;

            var BaseUrl = _configuration.GetSection("Urls")["BaseUrl"];

            if(BaseUrl == null) return string.Empty;

            var PicUrl = $"{BaseUrl}{source.PictureUrl}";

            return PicUrl;

        }
    }
}
