using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;

namespace ShoppingApp.Application.Features.CartItems.Queries
{
    public record GetCartItemByIdQuery(Ulid? CartItemId, Ulid? CartId, Ulid? ProductVariantId) : IRequest<CartItemDto?>;
}
