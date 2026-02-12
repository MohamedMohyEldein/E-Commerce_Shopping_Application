using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToken(string refreshToken, string userId, DateTime expiryDate)
        {
            var token = new RefreshToken
            {
                Id = Ulid.NewUlid(),
                Token = refreshToken,
                UserId = Ulid.Parse(userId),
                ExpiryDate = expiryDate,
                CreationDate = DateTime.UtcNow,
                IsRevoked = false
            };
            await _context.RefreshTokens.AddAsync(token);
        }

        public async Task RevokeRefreshToken(RefreshToken token)
        {
            token.IsRevoked = true;
            _context.RefreshTokens.Update(token);
            await Task.CompletedTask;
        }

        public async Task<RefreshToken?> FindToken(string refreshToken)
        {
           var token = await _context.RefreshTokens.Include(rf => rf.User).FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked && rt.ExpiryDate >= DateTime.UtcNow);

            return token;
        }
        public async Task ReplaceRefreshTokenAsync(RefreshToken oldRefreshToken, string newRefreshToken, DateTime expiresIn, CancellationToken cancellationToken)
        {

            oldRefreshToken.Token = newRefreshToken;
            oldRefreshToken.ExpiryDate = expiresIn;
            _context.Update(oldRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken, cancellationToken);
            if (token is null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
