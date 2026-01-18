using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
    public record LoginCommand(string? Email, string? Password) : IRequest<AuthResultDto>;
   
}
