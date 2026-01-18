namespace ShoppingApp.Domain.Entities
{
    public class OrderItem
    {
        public Ulid Id { get; set; }
        public Ulid OrderId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public Order? Order { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
