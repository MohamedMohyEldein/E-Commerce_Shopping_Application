using MediatR;
using ShoppingApp.Application.Features.Orders.DTOs;

namespace ShoppingApp.Application.Features.Orders.Queries
{
    public record GetAllUserOrdersQuery(Ulid? UserId) : IRequest<List<OrderDto>?>;
}
