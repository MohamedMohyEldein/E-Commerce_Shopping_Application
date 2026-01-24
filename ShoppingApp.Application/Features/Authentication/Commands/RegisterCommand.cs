using MediatR;
using ShoppingApp.Application.Features.Authentication.DTOs;

namespace ShoppingApp.Application.Features.Authentication.Commands
{
<<<<<<< HEAD
    public record RegisterCommand(string? Email, string? Password, string? FullName) : IRequest<AuthResultDto>;
=======
    public record RegisterCommand(RegisterDto? RegisterDto) : IRequest<AuthResultDto>;
>>>>>>> 5a54dd5
    
}
