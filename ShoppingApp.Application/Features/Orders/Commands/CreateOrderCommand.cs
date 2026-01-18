using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(CreateOrderDto? Order) : IRequest<OrderDto>;
}
