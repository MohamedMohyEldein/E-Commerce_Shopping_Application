namespace ShoppingApp.Application.Features.Authentication.DTOs
{
    public class RefreshTokenResultDto
    {
        public string JWTToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
