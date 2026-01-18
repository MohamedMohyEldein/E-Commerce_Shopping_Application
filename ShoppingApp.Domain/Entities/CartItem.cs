namespace ShoppingApp.Domain.Entities
{
    public class CartItem
    {
        public Ulid Id { get; set; }
        public Ulid CartId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public Cart? Cart { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
