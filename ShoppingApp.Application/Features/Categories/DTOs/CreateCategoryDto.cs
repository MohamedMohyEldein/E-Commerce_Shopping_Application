using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Features.Categories.DTOs
{
    public class CreateCategoryDto
    {
        public IFormFile? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
