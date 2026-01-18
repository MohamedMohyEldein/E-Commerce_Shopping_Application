using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Queries
{
    public record GetOrderItemByIdQuery(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId) : IRequest<OrderItemDto?>;
}
