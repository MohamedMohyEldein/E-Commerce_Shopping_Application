using ShoppingApp.Application.Features.CartItems.DTOs;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        public Task<bool> GetCartAndProductVarientByIdAsync(Ulid? CartId, Ulid? ProductVariantId);
        public Task<CartItemDto?> GetCartItemByIdAsync(Ulid? CartItemId, Ulid? CartId, Ulid? productVariantId);
        public Task<bool> RemoveCartItemAsync(Ulid? CartItemId, Ulid? CartId, Ulid? productVariantId);
        public Task<List<CartItemDto>?> GetAllCartItemsAsync(Ulid? CartId);
    }
}
