using MediatR;
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
        private readonly UserManager<AppUser> _userManager;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        public async Task<RefreshTokenResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.RefreshToken is null)
            {
                throw new BadRequestException("Refresh token is required.");
            }
            if(request.UserEmail is null)
            {
                throw new BadRequestException("User email is required.");
            }
            
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if(user is null)
            {
                throw new UnauthorizedException("User not found.");
            }

            var refToken = await _unitOfWork.RefreshToken.FindToken(request.RefreshToken, user.Id);

            if (refToken is null) throw new UnauthorizedException("Invalid refresh token.");


            await _unitOfWork.RefreshToken.RevokeRefreshToken(refToken);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            await _unitOfWork.RefreshToken.AddToken(refreshToken, user.Id, DateTime.UtcNow.AddMonths(6));

            var jwtToken = await _jwtTokenService.GenerateJwtToken(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new RefreshTokenResultDto
            {
                JWTToken = jwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
