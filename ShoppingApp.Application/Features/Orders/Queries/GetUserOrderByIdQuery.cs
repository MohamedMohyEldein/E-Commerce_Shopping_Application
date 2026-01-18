using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Queries
{
    public record GetUserOrderByIdQuery(Ulid UserId, Ulid OrderId) : IRequest<OrderDto?>;
}
