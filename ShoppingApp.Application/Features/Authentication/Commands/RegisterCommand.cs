using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Application.Features.Authentication.Commands
{

    public record RegisterCommand(RegisterDto? RegisterDto) : IRequest<AuthResultDto>;    
}
