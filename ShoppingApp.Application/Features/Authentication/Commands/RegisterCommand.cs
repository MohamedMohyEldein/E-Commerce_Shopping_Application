using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
    public record RegisterCommand(string? Email, string? Password, string? FullName) : IRequest<AuthResultDto>;
    
}
