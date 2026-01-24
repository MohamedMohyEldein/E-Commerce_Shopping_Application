using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Commands
{
    public record AssignUserToRoleCommand(string? email, string? roleName) : IRequest<bool>;
}
