using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly RoleManager<AppRole> _roleManager;
        public CreateRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.RoleName is null)
            {
                throw new BadRequestException("Role name cannot be null.");
            }
            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);

            if (roleExists)
            {
                throw new BadRequestException($"Role '{request.RoleName}' already exists.");
            }

<<<<<<< HEAD
            var result = await _roleManager.CreateAsync(new AppRole() {Name = request.RoleName, Id = Ulid.NewUlid()});
=======
            var result = await _roleManager.CreateAsync(new AppRole() {Name = request.RoleName, Id = Ulid.NewUlid().ToString()});
>>>>>>> 5a54dd5

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
