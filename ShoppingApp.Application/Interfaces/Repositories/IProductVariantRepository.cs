using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IProductVariantRepository
    {
        public Task<ProductVariant?> GetProductVariantByProductIdAsync(Ulid? productVariantId, Ulid? productId);
        public Task<List<ProductVariant>> GetAllProductVariantsByProductIdAsync(Ulid? productId);
    }
}
