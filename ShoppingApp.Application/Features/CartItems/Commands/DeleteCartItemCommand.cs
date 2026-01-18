using MediatR;

namespace ShoppingApp.Application.Features.CartItems.Commands
{
    public record DeleteCartItemCommand(Ulid? CartItemId, Ulid? CartId, Ulid? ProductVariantId) : IRequest<bool>;
}
