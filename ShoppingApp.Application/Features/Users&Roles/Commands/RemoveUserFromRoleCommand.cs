using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Commands
{
    public record RemoveUserFromRoleCommand(string? Email, string? RoleName) : IRequest<bool>;
}
