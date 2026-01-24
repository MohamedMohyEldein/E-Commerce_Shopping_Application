using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Queries
{
    public record GetAllRolesQuery() : IRequest<List<string>?>;
}
