namespace ShoppingApp.Domain.Entities
{
    public class WishlistItem
    {
        public Ulid Id { get; set; }
        public Ulid WishlistId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public Wishlist? Wishlist { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
