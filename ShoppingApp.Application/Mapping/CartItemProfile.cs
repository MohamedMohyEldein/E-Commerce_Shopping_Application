using AutoMapper;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CreateCartItemDto, CartItem>();
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
