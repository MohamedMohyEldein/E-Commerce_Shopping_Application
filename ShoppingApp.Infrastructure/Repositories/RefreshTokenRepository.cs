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
                UserId = userId,
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

        public async Task<RefreshToken?> FindToken(string refreshToken, string userId)
        {
           var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.User.Id == userId && !rt.IsRevoked && rt.ExpiryDate >= DateTime.UtcNow);

            return token;
        }
    }
}
