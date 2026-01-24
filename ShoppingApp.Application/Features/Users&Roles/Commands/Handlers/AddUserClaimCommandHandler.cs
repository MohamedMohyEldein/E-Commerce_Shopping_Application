using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Users_Roles.Commands.Handlers
{
    public class AddUserClaimCommandHandler : IRequestHandler<AddUserClaimCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public AddUserClaimCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            if(request.UserEmail is null || request.ClaimType is null || request.ClaimValue is null)
            {
                throw new BadRequestException("UserEmail, ClaimType and ClaimValue must be provided.");
            }

            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if(user is null)
            {
                throw new UnauthorizedException("User not found.");
            }

            var claim = new Claim(request.ClaimType, request.ClaimValue);

            var result = await _userManager.AddClaimAsync(user, claim);

            return result.Succeeded ? true : false;
        }
    }
}
