namespace ShoppingApp.Application.Features.Categories.DTOs
{
    public class CategoryDto
    {
        public Ulid Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
