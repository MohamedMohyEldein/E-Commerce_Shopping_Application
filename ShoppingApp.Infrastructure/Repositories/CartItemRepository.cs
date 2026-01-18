using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Features.CartItems.DTOs;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartItemRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GetCartAndProductVarientByIdAsync(Ulid? CartId, Ulid? ProductVariantId)
        {
            var entity = await _context.CartItems
               .Include(p => p.Cart)
               .Include(p => p.ProductVariant)
               .FirstOrDefaultAsync(p => p.CartId == CartId && p.ProductVariantId == ProductVariantId);

            if(entity is null) return false;

            return true;
        }
        public async Task<List<CartItemDto>?> GetAllCartItemsAsync(Ulid? CartId)
        {
            return await _context.CartItems.Where(p => p.CartId == CartId)
               .Include(p => p.ProductVariant)
               .ThenInclude(p => p.Product)
               .Select(p => new CartItemDto { ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity })
               .ToListAsync();
        }

        public async Task<CartItemDto?> GetCartItemByIdAsync(Ulid? CartItemId, Ulid? CartId, Ulid? productVariantId)
        {
            return await _context.CartItems
                 .Include(p => p.Cart)
                 .Include(p => p.ProductVariant)
                 .Select(p => new CartItemDto { ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity })
                 .FirstOrDefaultAsync(p => p.Id == CartItemId && p.CartId == CartId && p.ProductVariantId == productVariantId);
        }

        public async Task<bool> RemoveCartItemAsync(Ulid? CartItemId, Ulid? CartId, Ulid? productVariantId)
        {
            var cartItem = await GetCartItemByIdAsync(CartItemId, CartId, productVariantId);

            if (cartItem is null) return false;

            var entity = _mapper.Map<CartItem>(cartItem);
            _context.CartItems.Remove(entity);
            return true;
        }
    }
}
