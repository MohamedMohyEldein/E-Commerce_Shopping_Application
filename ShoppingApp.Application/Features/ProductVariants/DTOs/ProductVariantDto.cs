using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Features.ProductVariants.DTOs
{
    public class ProductVariantDto
    {
        public Ulid Id { get; set; }
        public Ulid ProductId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int StockQuantity { get; set; }
    }
}
