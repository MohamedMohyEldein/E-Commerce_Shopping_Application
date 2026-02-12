using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Authentication.Commands.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public LogoutCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if(request.RefreshToken is null)
            {
                throw new BadRequestException("Refresh token is required.");
            }
            await _unitOfWork.RefreshToken.DeleteRefreshTokenAsync(request.RefreshToken, new CancellationToken());
        }
    }
}
