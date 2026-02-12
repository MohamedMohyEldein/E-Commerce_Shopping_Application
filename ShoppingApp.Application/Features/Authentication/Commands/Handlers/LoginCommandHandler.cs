﻿using MediatR;
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
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.LoginDto is null)
            {
                throw new BadRequestException("Login DTO cannot be null!");
            }

            if (request.LoginDto.Email is null || request.LoginDto.Password is null)
            {
                throw new BadRequestException("Email and Password cannot be null!");
            }

            AppUser? user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }


            var signInResult = await _signInManager.PasswordSignInAsync(user, request.LoginDto.Password, isPersistent: true, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var refreshTokenGenerated = _jwtTokenService.GenerateRefreshToken();

            await _unitOfWork.RefreshToken.AddToken(refreshTokenGenerated, user.Id.ToString(), DateTime.UtcNow.AddDays(7));
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var authResult = new AuthResultDto
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                FullName = user.FullName,
                Token = await _jwtTokenService.GenerateJwtToken(user),
                RefreshToken = refreshTokenGenerated
            };
            return authResult;
        }
    }
}
