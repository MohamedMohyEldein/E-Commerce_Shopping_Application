using System;
namespace ShoppingApp.Domain.Entities
{
    public class Category
    {
        public Ulid Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
