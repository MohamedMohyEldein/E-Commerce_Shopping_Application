using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;

namespace ShoppingApp.Application.Features.CartItems.Queries
{
    public record GetAllCartItemsQuery(Ulid? CartId) : IRequest<List<CartItemDto?>>;
}
