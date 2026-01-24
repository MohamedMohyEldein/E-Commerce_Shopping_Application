using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;    
        private readonly RoleManager<AppRole> _roleManager;
        public RemoveUserFromRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            if(request.Email is null || request.RoleName is null)
            {
                throw new BadRequestException("Email and RoleName must be provided.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                throw new NotFoundException($"User with email {request.Email} not found.");
            }

            var role = _roleManager.FindByNameAsync(request.RoleName);

            if(role is null)
            {
                throw new NotFoundException($"Role {request.RoleName} not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            return result.Succeeded ? true : false;
        }
    }
}
