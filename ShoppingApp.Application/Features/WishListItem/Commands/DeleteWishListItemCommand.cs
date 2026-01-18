using MediatR;

namespace ShoppingApp.Application.Features.WishListItem.Commands
{
    public record DeleteWishListItemCommand(Ulid? WishListItemId, Ulid? WishListId, Ulid? ProductVariantId) : IRequest<bool>;
}
