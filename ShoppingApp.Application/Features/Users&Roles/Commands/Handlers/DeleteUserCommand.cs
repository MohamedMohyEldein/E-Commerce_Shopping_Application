using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public record DeleteUserCommand(string? Email) : IRequest<bool>;
}
