using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem?> GetOrderAndVariantByIdAsync(Ulid? orderId, Ulid? productVariantId)
        {
           return await _context.OrderItems
                .Include(p => p.Order)
                .Include(p => p.ProductVariant)
                .FirstOrDefaultAsync(p => p.OrderId == orderId && p.ProductVariantId == productVariantId);
        }
        public async Task<OrderItemDto?> GetOrderItemByIdAsync(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId)
        {
            return await _context.OrderItems
                 .Include(p => p.Order)
                 .Include(p => p.ProductVariant)
                 .Select(p => new OrderItemDto { ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity })
                 .FirstOrDefaultAsync(p => p.Id == orderItemId && p.OrderId == orderId && p.ProductVariantId == productVariantId);
        }
        public async Task<List<OrderItemDto>?> GetAllOrderItemsAsync(Ulid? orderId)
        {
            return await _context.OrderItems.Where(p => p.OrderId == orderId)
                .Include(p => p.ProductVariant)
                .ThenInclude(p => p.Product)
                .Select(p => new OrderItemDto{ImageUrl = p.ProductVariant.ImageUrl, Name = p.ProductVariant.Product.Name, Description = p.ProductVariant.Product.Description, Color = p.ProductVariant.Color, Size = p.ProductVariant.Size, Price = p.ProductVariant.Product.Price, Quantity = p.Quantity})
                .ToListAsync();
        }

        

        public async Task<bool> RemoveOrderItemAsync(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId)
        {
           var orderItem = await GetOrderItemByIdAsync(orderItemId, orderId, productVariantId);

            if (orderItem is null)
            {
                return false;
            }
            var entity = _mapper.Map<OrderItem>(orderItem);

            _context.OrderItems.Remove(entity);
            return true;
        }
    }
}
