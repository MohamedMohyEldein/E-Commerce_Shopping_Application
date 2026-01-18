using AutoMapper;
using ShoppingApp.Application.Features.ProductVariants.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class ProductVariantProfile : Profile
    {
        public ProductVariantProfile()
        {
            CreateMap<CreateProductVariantDto, ProductVariant>();
            CreateMap<UpdateProductVariantDto, ProductVariant>();
            CreateMap<ProductVariant, ProductVariantDto>();
        }
    }
}
