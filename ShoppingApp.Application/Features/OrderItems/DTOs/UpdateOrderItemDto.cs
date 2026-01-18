namespace ShoppingApp.Application.Features.OrderItems.DTOs
{
    public class UpdateOrderItemDto
    {
        public Ulid OrderId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
