namespace ShoppingApp.Application.Features.Authentication.DTOs
{
    public class AuthResultDto
    {
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? RefreshToken { get; set; }
    }
}
