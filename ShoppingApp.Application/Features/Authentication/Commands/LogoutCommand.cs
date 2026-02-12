using MediatR;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
    public record LogoutCommand(string RefreshToken) : IRequest;
}
