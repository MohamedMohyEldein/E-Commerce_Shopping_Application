using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<ProductVariant> ProductVariants { get; }
        public IGenericRepository<Cart> Carts { get; }
        public IGenericRepository<CartItem> CartItems { get; }
        public IGenericRepository<Wishlist> Wishlists { get; }
        public IGenericRepository<WishlistItem> WishlistItems { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderItem> OrderItems { get; }
        public IGenericRepository<AppUser> Users { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IWishListItemRepository WishListItemRepository { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new GenericRepository<Category>(_context);
            Products = new GenericRepository<Product>(_context);
            ProductVariants = new GenericRepository<ProductVariant>(_context);
            Carts = new GenericRepository<Cart>(_context);
            CartItems = new GenericRepository<CartItem>(_context);
            Wishlists = new GenericRepository<Wishlist>(_context);
            WishlistItems = new GenericRepository<WishlistItem>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems = new GenericRepository<OrderItem>(_context);
            Users = new GenericRepository<AppUser>(_context);
            OrderRepository = new OrderRepository(_context);
            ProductVariantRepository = new ProductVariantRepository(_context);
            OrderItemRepository = new OrderItemRepository(_context);
            CartItemRepository = new CartItemRepository(_context);
            WishListItemRepository = new WishListItemRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
