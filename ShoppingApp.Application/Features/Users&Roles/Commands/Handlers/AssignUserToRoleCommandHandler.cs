using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public class AssignUserToRoleCommandHandler : IRequestHandler<AssignUserToRoleCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AssignUserToRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            if(request.email is null || request.roleName is null)
            {
                throw new BadRequestException("Email or RoleName cannot be null");
            }

            var user = await _userManager.FindByEmailAsync(request.email);

            if(user is null)
            {
                throw new BadRequestException($"User with email {request.email} not found");
            }

            var roleExists = await _roleManager.RoleExistsAsync(request.roleName);

            if(!roleExists)
            {
                throw new BadRequestException($"Role {request.roleName} does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, request.roleName);

            return result.Succeeded ? true : false;
        }
    }
}
