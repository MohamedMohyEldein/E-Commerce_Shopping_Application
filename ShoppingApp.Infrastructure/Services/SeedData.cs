using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Enums;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Services
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            // ensure database exists / migrations applied (optional)
            await db.Database.EnsureCreatedAsync();

            // if already seeded, bail out
            if (db.Users.Any()) return;

            // create user
            var user = new AppUser
            {
                Id = Ulid.NewUlid().ToString(),        // runtime ULID
                UserName = "testuser",
                NormalizedUserName = "TESTUSER",
                Email = "test@shop.com",
                NormalizedEmail = "TEST@SHOP.COM",
                EmailConfirmed = true,
                PasswordHash = "FAKE_HASH"
            };

            // categories
            var cat1 = new Category
            {
                Id = Ulid.NewUlid(),
                Name = "Electronics",
                Description = "Electronic devices",
                ImageUrl = "/images/categories/electronics.jpg"
            };

            var cat2 = new Category
            {
                Id = Ulid.NewUlid(),
                Name = "Clothing",
                Description = "Apparel",
                ImageUrl = "/images/categories/clothing.jpg"
            };

            // products
            var prod1 = new Product
            {
                Id = Ulid.NewUlid(),
                CategoryId = cat1.Id,
                Name = "Smartphone",
                Description = "Latest model",
                Price = 999,
                StockQuantity = 50,
                ImageUrl = "/images/products/smartphone.jpg",
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = new DateTime(2024, 1, 1)
            };

            var prod2 = new Product
            {
                Id = Ulid.NewUlid(),
                CategoryId = cat2.Id,
                Name = "T-Shirt",
                Description = "Cotton t-shirt",
                Price = 29,
                StockQuantity = 200,
                ImageUrl = "/images/products/tshirt.jpg",
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = new DateTime(2024, 1, 1)
            };

            // product variants
            var pv1 = new ProductVariant
            {
                Id = Ulid.NewUlid(),
                ProductId = prod1.Id,
                Color = "Black",
                Size = "128GB",
                StockQuantity = 25,
                ImageUrl = "/images/variants/phone_black.jpg"
            };

            var pv2 = new ProductVariant
            {
                Id = Ulid.NewUlid(),
                ProductId = prod2.Id,
                Color = "Red",
                Size = "L",
                StockQuantity = 80,
                ImageUrl = "/images/variants/shirt_red.jpg"
            };

            // cart + item
            var cart = new Cart { Id = Ulid.NewUlid(), UserId = user.Id };
            var cartItem = new CartItem { Id = Ulid.NewUlid(), CartId = cart.Id, ProductVariantId = pv2.Id, Quantity = 2 };

            // wishlist + item
            var wishlist = new Wishlist { Id = Ulid.NewUlid(), UserId = user.Id };
            var wishlistItem = new WishlistItem { Id = Ulid.NewUlid(), WishlistId = wishlist.Id, ProductVariantId = prod1.Id };

            // order + item
            var order = new Order { Id = Ulid.NewUlid(), UserId = user.Id, OrderDate = new DateTime(2024, 2, 10), Status = OrderStatus.Pending};
            var orderItem = new OrderItem { Id = Ulid.NewUlid(), OrderId = order.Id, ProductVariantId = pv1.Id, Quantity = 2 };

            // Add to context
            db.Users.Add(user);
            db.Categories.AddRange(cat1, cat2);
            db.Products.AddRange(prod1, prod2);
            db.ProductVariants.AddRange(pv1, pv2);
            db.Carts.Add(cart);
            db.CartItems.Add(cartItem);
            db.Wishlists.Add(wishlist);
            db.WishlistItems.Add(wishlistItem);
            db.Orders.Add(order);
            db.OrderItems.Add(orderItem);

            await db.SaveChangesAsync();
        }
    }
}
