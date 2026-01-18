using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Application.Common.Settings;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Common.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        public string GenerateJwtToken(AppUser? user)
        {

            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Exp, expiryDate.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Ulid.NewUlid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiryDate,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string generatedToken = tokenHandler.WriteToken(token);
            return generatedToken;
        }
    }
}
