using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Application.Features.Authentication.DTOs
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = null!;
    }
}
