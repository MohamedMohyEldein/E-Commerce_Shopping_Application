namespace ShoppingApp.Application.Features.OrderItems.DTOs
{
    public class OrderItemDto
    {
        public Ulid Id { get; set; }
        public Ulid OrderId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
