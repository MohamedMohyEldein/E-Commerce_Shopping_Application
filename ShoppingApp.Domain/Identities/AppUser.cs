using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Domain.Identities
{
    public class AppUser : IdentityUser<Ulid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
