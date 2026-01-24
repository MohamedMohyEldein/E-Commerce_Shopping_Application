using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Commands
{
    public record CreateRoleCommand(string? RoleName) : IRequest<bool>;
}
