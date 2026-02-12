using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken?> FindToken(string refreshToken);
        public Task AddToken(string refreshToken, string userId, DateTime expiryDate);
        public Task RevokeRefreshToken(RefreshToken refreshToken);
        public Task ReplaceRefreshTokenAsync(RefreshToken oldRefreshToken, string newRefreshToken, DateTime expiresIn, CancellationToken cancellationToken);
        public Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
