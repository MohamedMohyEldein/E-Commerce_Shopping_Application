using AutoMapper;
using ShoppingApp.Application.Features.Categories.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
