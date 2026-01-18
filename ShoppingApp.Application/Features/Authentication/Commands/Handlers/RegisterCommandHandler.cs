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
            if (request is null)
            {
                throw new BadRequestException("Request cannot be null.");
            }
            if (request.Email is null || request.Password is null)
            { 
                throw new BadRequestException("Email and Password cannot be null."); 
            }

            AppUser? userExists = await _userManager.FindByEmailAsync(request.Email);

            if (userExists != null)
            {
                throw new ConflictException("User with this email already exists.");
            }

            var names = request.FullName.Split(' ');
            var firstName = names.First();
            var lastName = string.Join(' ', names.Skip(1));
            var identityResult = await _userManager.CreateAsync(new AppUser
            {
                Id = Ulid.NewUlid(),
                UserName = request.Email,
                Email = request.Email,
                FirstName = firstName,
                LastName = lastName
            }, request.Password);

            if (identityResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    throw new BadRequestException("User creation failed. Please check the provided details.");
                }

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
                    Token = _jwtTokenService.GenerateJwtToken(user)
                };

                return authResult;
            }
            else
            {
                throw new BadRequestException("User creation failed. Please check the provided details.");
            }
        }
    }
}
