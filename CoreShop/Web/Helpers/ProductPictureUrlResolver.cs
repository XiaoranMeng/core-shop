using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Web.DTOs;

namespace Web.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            return _configuration["BaseUrl"] + source.PictureUrl;
        }
    }
}
