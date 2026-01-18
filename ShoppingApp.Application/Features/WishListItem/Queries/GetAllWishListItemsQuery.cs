using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;

namespace ShoppingApp.Application.Features.WishListItem.Queries
{
    public record GetAllWishListItemsQuery(Ulid? WishListId) : IRequest<List<WishListItemDto?>>;
}
