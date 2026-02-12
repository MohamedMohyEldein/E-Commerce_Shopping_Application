using Microsoft.EntityFrameworkCore;
using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Order?> GetUserOrderByIdAsync(Ulid? orderId, Ulid? userId)
        {
            return await _context.Orders.FirstOrDefaultAsync(p => p.Id == orderId && p.UserId == userId);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Order>> GetAllUserOrdersAsync(Ulid? UserId)
        {
            return await _context.Orders.Where(o => o.UserId == UserId).ToListAsync();
        }
    }
}
