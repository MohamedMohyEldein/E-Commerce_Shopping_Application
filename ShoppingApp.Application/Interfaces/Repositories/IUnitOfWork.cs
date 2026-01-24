using ShoppingApp.Application.Interfaces.Repositories;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Identities;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<CartItem> CartItems { get; }
    public IGenericRepository<Cart> Carts { get; }
    public IGenericRepository<Category> Categories { get; }
    public IGenericRepository<OrderItem> OrderItems { get; }
    public IGenericRepository<Order> Orders { get; }
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<ProductVariant> ProductVariants { get; }
    public IGenericRepository<WishlistItem> WishlistItems { get; }
    public IGenericRepository<Wishlist> Wishlists { get; }
    public IGenericRepository<AppUser> Users { get; }
    public IOrderRepository OrderRepository { get; }
    public IProductVariantRepository ProductVariantRepository { get; }
    public IOrderItemRepository OrderItemRepository { get; }
    public ICartItemRepository CartItemRepository { get; }
    public IWishListItemRepository WishListItemRepository { get; }
    public IRefreshTokenRepository RefreshToken { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
