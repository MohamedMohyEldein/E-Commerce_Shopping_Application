using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
    public record RefreshTokenCommand(string? RefreshToken, string? UserEmail) : IRequest<RefreshTokenResultDto>;
}
