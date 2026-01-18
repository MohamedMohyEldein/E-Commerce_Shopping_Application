using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Authentication.DTOs;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Authentication.Commands.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request is null) 
            { 
                throw new BadRequestException("Login request cannot be null!");
            }

            if (request.Email is null || request.Password is null)
            { 
                throw new BadRequestException("Email and Password must be provided!"); 
            }

            AppUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var authResult = new AuthResultDto
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                Token = _jwtTokenService.GenerateJwtToken(user)
            };
            return authResult;
        }
    }
}
