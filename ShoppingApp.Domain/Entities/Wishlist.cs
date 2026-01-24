using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Domain.Entities
{
    public class Wishlist
    {
        public Ulid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<WishlistItem>? Items { get; set; }
        public AppUser User { get; set; }
    }
}
