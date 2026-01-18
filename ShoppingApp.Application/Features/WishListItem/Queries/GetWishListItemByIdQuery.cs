using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;

namespace ShoppingApp.Application.Features.WishListItem.Queries
{
    public record GetWishListItemByIdQuery(Ulid? WishListItemId, Ulid? WishListId, Ulid? ProductVariantId) : IRequest<WishListItemDto?>;
}
