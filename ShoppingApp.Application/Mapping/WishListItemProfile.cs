using AutoMapper;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class WishListItemProfile : Profile
    {
        public WishListItemProfile()
        {
            CreateMap<CreateWishListItemDto, WishlistItem>();
            CreateMap<WishlistItem, WishListItemDto>();
        }
    }
}
