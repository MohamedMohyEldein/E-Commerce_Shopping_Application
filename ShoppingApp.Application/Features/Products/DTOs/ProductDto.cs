using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Features.Products.DTOs
{
    public class ProductDto
    {
        public Ulid Id { get; set; }
        public Ulid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
