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
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes));

            CreateMap<ProductsSizes, ProductsSizeDto>()
                .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size != null ? src.Size.SizeName : "Sin nombre"))  // ← ESTO ES CRÍTICO
                .ForMember(dest => dest.SizeStock, opt => opt.MapFrom(src => src.SizeStock))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProductResponseDto, Product>();




        }


    }
}
