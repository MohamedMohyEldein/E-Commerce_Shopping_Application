using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IDisposable
    {
        Task<Order?> GetUserOrderByIdAsync(Ulid? orderId, Ulid? userId);
        Task<List<Order>> GetAllUserOrdersAsync(Ulid? UserId);
    }
}
