﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Authentication.DTOs;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Authentication.Commands.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RefreshTokenResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.RefreshToken is null)
            {
                throw new BadRequestException("Refresh token is required.");
            }

            var oldRefreshToken = await _unitOfWork.RefreshToken.FindToken(request.RefreshToken);

            if (oldRefreshToken is null) throw new UnauthorizedException("Invalid refresh token.");


            var jwtToken = await _jwtTokenService.GenerateJwtToken(oldRefreshToken.User);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            await _unitOfWork.RefreshToken.ReplaceRefreshTokenAsync(oldRefreshToken, newRefreshToken, DateTime.UtcNow.AddDays(7), new CancellationToken());

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new RefreshTokenResultDto
            {
                JWTToken = jwtToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
