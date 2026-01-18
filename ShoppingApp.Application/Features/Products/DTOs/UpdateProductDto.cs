using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Features.Products.DTOs
{
    public class UpdateProductDto
    {
        public Ulid? CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
    }
}
