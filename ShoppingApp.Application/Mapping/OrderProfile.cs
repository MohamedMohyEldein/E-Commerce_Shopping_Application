using AutoMapper;
using ShoppingApp.Application.Features.Orders.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();
            CreateMap<Order, OrderDto>();
        }
    }
}
