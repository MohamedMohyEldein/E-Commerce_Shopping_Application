namespace ShoppingApp.Domain.Entities
{
    public class ProductVariant
    {
        public Ulid Id { get; set; }
        public Ulid ProductId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int StockQuantity { get; set; }
        public Product? Product { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
