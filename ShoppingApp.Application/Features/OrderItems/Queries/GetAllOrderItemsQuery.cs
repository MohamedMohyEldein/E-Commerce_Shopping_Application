using MediatR;
using ShoppingApp.Application.Features.OrderItems.DTOs;

namespace ShoppingApp.Application.Features.OrderItems.Queries
{
    public record GetAllOrderItemsQuery(Ulid? orderId) : IRequest<List<OrderItemDto>?>;
}
