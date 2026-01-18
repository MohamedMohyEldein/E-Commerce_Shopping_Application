namespace ShoppingApp.Application.Features.CartItems.DTOs
{
    public class CreateCartItemDto
    {
        public Ulid CartId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
