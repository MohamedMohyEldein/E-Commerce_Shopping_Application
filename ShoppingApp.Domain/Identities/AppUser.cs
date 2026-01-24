using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Entities;

namespace ShoppingApp.Domain.Identities
{
    public class AppUser : IdentityUser<string>
    {
        [Required]
        public string? FullName { get; set; }
<<<<<<< HEAD
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? password { get; set; }
=======
>>>>>>> 5a54dd5
        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}
