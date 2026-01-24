using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Email is null)
            {
                throw new BadRequestException($"Email cannot be null.");
            }
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                throw new NotFoundException($"User with email {request.Email} not found.");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? true : false;

        }
    }
}
