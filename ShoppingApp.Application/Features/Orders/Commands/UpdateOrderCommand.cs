using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Commands
{
    public record UpdateOrderCommand(Ulid? Id, UpdateOrderDto Order) : IRequest<OrderDto>;
}
