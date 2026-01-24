using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Commands
{
    public record AddUserClaimCommand(string? UserEmail, string? ClaimType, string? ClaimValue) : IRequest<bool>;
}
