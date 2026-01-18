using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Commands
{
    public record CreateOrderItemcommand(CreateOrderItemDto? OrderItemDto) : IRequest<OrderItemDto?>;
}
