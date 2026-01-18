using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductVariantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariant>> GetAllProductVariantsByProductIdAsync(Ulid? productId)
        {
            return await _context.ProductVariants.Where(o => o.ProductId == productId).ToListAsync();
        }

        public async Task<ProductVariant?> GetProductVariantByProductIdAsync(Ulid? productVariantId, Ulid? productId)
        {
            return await _context.ProductVariants.FirstOrDefaultAsync(o => o.Id == productVariantId && o.ProductId == productId);
        }
    }
}
