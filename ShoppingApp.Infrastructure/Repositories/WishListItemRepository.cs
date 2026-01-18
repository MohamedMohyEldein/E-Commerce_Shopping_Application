using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Features.WishListItem.DTOs;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class WishListItemRepository : IWishListItemRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public WishListItemRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public WishListItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> GetWishListAndProductVarientByIdAsync(Ulid? WishListId, Ulid? ProductVariantId)
        {
            var entity = await _context.WishlistItems
               .Include(p => p.Wishlist)
               .Include(p => p.ProductVariant)
               .FirstOrDefaultAsync(p => p.WishlistId == WishListId && p.ProductVariantId == ProductVariantId);

            if (entity is null) return false;

            return true;
        }
        public async Task<List<WishListItemDto>?> GetAllWishListItemsAsync(Ulid? WishListId)
        {
            return await _context.WishlistItems.Where(p => p.WishlistId == WishListId)
              .Include(p => p.ProductVariant)
              .ThenInclude(p => p.Product)
              .Select(p => new WishListItemDto { ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity })
              .ToListAsync();
        }

        public async Task<WishListItemDto?> GetWishListItemByIdAsync(Ulid? WishListItemId, Ulid? WishListId, Ulid? productVariantId)
        {
            return await _context.WishlistItems
                 .Include(p => p.Wishlist)
                 .Include(p => p.ProductVariant)
                 .Select(p => new WishListItemDto { ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity })
                 .FirstOrDefaultAsync(p => p.Id == WishListItemId && p.WishListId == WishListId && p.ProductVariantId == productVariantId);
        }

        public async Task<bool> RemoveWishListItemAsync(Ulid? WishListItemId, Ulid? WishListId, Ulid? productVariantId)
        {
            var wishListItem = await GetWishListItemByIdAsync(WishListItemId, WishListId, productVariantId);

            if (wishListItem is null) return false;

            var entity = _mapper.Map<CartItem>(wishListItem);
            _context.CartItems.Remove(entity);
            return true;
        }
    }
}
