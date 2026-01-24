using MediatR;

namespace ShoppingApp.Application.Features.Users_Roles.Queries
{
    public record GetUserRolesQuery(string? Email) : IRequest<List<string>?>;
}
