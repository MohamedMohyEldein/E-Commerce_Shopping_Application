using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Features.Authentication.DTOs;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Authentication.Commands.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.RegisterDto is null)
            {
                throw new BadRequestException("Register DTO cannot be null.");
            }
            if (request.RegisterDto.Email is null || request.RegisterDto.Password is null || request.RegisterDto.FullName is null)
            { 
                throw new BadRequestException("Email, Password and full name cannot be null."); 
            }

            AppUser? userExists = await _userManager.FindByEmailAsync(request.RegisterDto.Email);

            if (userExists != null)
            {
                throw new ConflictException("User with this email already exists.");
            }
            
            var user = new AppUser
            {
                Id = Ulid.NewUlid().ToString(),
                UserName = request.RegisterDto.Email,
                Email = request.RegisterDto.Email,
                FullName = request.RegisterDto.FullName,
            };

            var identityResult = await _userManager.CreateAsync(user, request.RegisterDto.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "AppUser");

                var cart = new Cart
                {
                    Id = Ulid.NewUlid(),
                    UserId = user.Id
                };
                var wishlist = new Wishlist
                {
                    Id = Ulid.NewUlid(),
                    UserId = user.Id
                };

                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.Wishlists.AddAsync(wishlist);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                AuthResultDto authResult = new AuthResultDto
                {
                    Email = user.Email,
                    UserId = user.Id.ToString(),
                    Token = await _jwtTokenService.GenerateJwtToken(user),
                    FullName = user.FullName,
                };

                return authResult;
            }
            else
            {
                var errors = string.Join("\n", identityResult.Errors.Select(e => e.Description));
                throw new BadRequestException($"User creation failed: {errors}");
            }
        }
    }
}
