using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Features.ProductVariants.DTOs
{
    public class CreateProductVariantDto
    {
        public Ulid ProductId { get; set; }
        public IFormFile? Image { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int StockQuantity { get; set; }
    }
}
