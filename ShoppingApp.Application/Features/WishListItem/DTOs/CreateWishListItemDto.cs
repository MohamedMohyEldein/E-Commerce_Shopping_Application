namespace ShoppingApp.Application.Features.WishListItem.DTOs
{
    public class CreateWishListItemDto
    {
        public Ulid WishlistId { get; set; }
        public Ulid ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
