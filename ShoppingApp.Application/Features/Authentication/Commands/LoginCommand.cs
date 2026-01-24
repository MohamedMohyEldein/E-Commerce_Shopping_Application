using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
    public record LoginCommand(LoginDto? LoginDto) : IRequest<AuthResultDto>;
   
}
