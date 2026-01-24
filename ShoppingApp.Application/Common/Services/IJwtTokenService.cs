using System.Security.Claims;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Common.Services
{
    public interface IJwtTokenService
    {
        public Task<string> GenerateJwtToken(AppUser? user);
        public Task<List<Claim>> GetClaimsAsync(AppUser? user);
        public string GenerateRefreshToken();
    }
}