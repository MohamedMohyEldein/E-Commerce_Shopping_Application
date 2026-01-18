using MediatR;

namespace ShoppingApp.Application.Features.OrderItems.Commands
{
    public record DeleteOrderItemcommand(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId) : IRequest<bool>;
}
