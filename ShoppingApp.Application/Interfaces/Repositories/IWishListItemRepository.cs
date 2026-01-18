using ShoppingApp.Application.Features.WishListItem.DTOs;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IWishListItemRepository
    {
        public Task<bool> GetWishListAndProductVarientByIdAsync(Ulid? WishListId, Ulid? ProductVariantId);
        public Task<WishListItemDto?> GetWishListItemByIdAsync(Ulid? WishListItemId, Ulid? WishListId, Ulid? productVariantId);
        public Task<bool> RemoveWishListItemAsync(Ulid? WishListItemId, Ulid? WishListId, Ulid? productVariantId);
        public Task<List<WishListItemDto>?> GetAllWishListItemsAsync(Ulid? WishListId);
    }
}
