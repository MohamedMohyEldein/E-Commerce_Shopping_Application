using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken?> FindToken(string refreshToken, string userId);
        public Task AddToken(string refreshToken, string userId, DateTime expiryDate);
        public Task RevokeRefreshToken(RefreshToken refreshToken);
    }
}
