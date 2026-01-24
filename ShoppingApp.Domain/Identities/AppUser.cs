using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Domain.Identities
{
    public class AppUser : IdentityUser<Ulid>
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? password { get; set; }
        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
