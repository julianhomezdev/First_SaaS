using AutoMapper;
using SaaS.src.Application.DTOs.Product;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.ProductSizes));

            CreateMap<ProductsSizes, ProductsSizeDto>()
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.SizeName));


            CreateMap<ProductResponseDto, Product>();




        }


    }
}
