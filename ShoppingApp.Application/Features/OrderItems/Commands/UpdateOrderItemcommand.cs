using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Commands
{
    public record UpdateOrderItemcommand(Ulid? orderItemId, UpdateOrderItemDto? OrderItemDto) : IRequest<OrderItemDto?>;
}
