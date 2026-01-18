using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Common.Services
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(AppUser? user);
    }
}