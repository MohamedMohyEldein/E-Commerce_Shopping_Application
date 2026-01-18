using MediatR;
using ShoppingApp.Application.Features.WishListItem.DTOs;

namespace ShoppingApp.Application.Features.WishListItem.Commands
{
    public record CreateWishListItemCommand(CreateWishListItemDto? CreateWishListItemDto) : IRequest<WishListItemDto?>;
}
