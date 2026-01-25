using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Domain.Identities
{
    public class AppUser : IdentityUser<string>
    {
        [Required]
        public string? FullName { get; set; }

        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}
