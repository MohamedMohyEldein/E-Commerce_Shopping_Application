using AutoMapper;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<UpdateOrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
