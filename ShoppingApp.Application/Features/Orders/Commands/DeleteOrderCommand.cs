using MediatR;

namespace ShoppingApp.Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(Ulid? Id, Ulid? UserId) : IRequest<bool>;
}
