using ShoppingApp.Application.Features.OrderItems.DTOs;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        public Task<OrderItem?> GetOrderAndVariantByIdAsync(Ulid? orderId, Ulid? productVariantId);
        public Task<OrderItemDto?> GetOrderItemByIdAsync(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId);
        public Task<bool> RemoveOrderItemAsync(Ulid? orderItemId, Ulid? orderId, Ulid? productVariantId);
        public Task<List<OrderItemDto>?> GetAllOrderItemsAsync(Ulid? orderId);
    }
}
