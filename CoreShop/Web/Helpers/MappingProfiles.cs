using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
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
                    options.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureUrl, options =>
                    options.MapFrom<ProductPictureUrlResolver>())
                .ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
        }
    }
}
