namespace ShoppingApp.Domain.Entities
{
    public class Product
    {
        public Ulid Id { get; set; }
        public Ulid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Category? Category { get; set; }
        public ICollection<ProductVariant>? Variants { get; set; }
    }
}
