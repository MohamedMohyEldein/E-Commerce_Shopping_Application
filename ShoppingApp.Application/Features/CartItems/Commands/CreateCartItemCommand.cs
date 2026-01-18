using MediatR;
using ShoppingApp.Application.Features.CartItems.DTOs;

namespace ShoppingApp.Application.Features.CartItems.Commands
{
    public record CreateCartItemCommand(CreateCartItemDto? CreateCartItemDto) : IRequest<CartItemDto?>;
}
