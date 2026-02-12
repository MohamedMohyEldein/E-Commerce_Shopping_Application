using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Features.Users_Roles.DTOs;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Queries.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>?>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetAllUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDTO>?> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDto = new List<UserDTO>();
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                usersDto.Add(new UserDTO
                {
                    Id = user.Id.ToString(),
                    UserName = user.FullName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return usersDto;
        }
    }
}
