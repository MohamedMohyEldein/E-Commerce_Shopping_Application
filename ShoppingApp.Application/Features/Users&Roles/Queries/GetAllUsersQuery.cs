using MediatR;
using ShoppingApp.Application.Features.Users_Roles.DTOs;

namespace ShoppingApp.Application.Features.Users_Roles.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserDTO>?>;
}
