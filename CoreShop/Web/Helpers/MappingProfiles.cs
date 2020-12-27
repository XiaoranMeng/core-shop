using AutoMapper;
using Core.Entities;
using Web.DTOs;

namespace Web.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(destination => destination.ProductBrand, options => 
                    options.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.ProductType, options =>
                    options.MapFrom(source => source.ProductType.Name));
        }
    }
}
