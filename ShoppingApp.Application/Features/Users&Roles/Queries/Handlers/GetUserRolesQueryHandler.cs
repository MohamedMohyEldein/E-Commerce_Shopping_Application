using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Queries.Handlers
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<string>?>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserRolesQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>?> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            if (request.Email is null)
            {
                throw new BadRequestException("Email cannot be null.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new BadRequestException($"User with email {request.Email} not found.");
            }

            var rolesNames = await _userManager.GetRolesAsync(user);

            return rolesNames.ToList();
        }
    }
}
