using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Application.Common.Settings;
using ShoppingApp.Domain.Exceptions;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Application.Common.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> GenerateJwtToken(AppUser? user)
        {
            if (user is null)
            {
                throw new BadRequestException("User cannot be null");
            }

            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var claims = await GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
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

        // Helper method to get claims for the user
        public async Task<List<Claim>> GetClaimsAsync(AppUser? user)
        {
            if (user is null)
            {
                throw new BadRequestException("User cannot be null");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(JwtRegisteredClaimNames.Jti, Ulid.NewUlid().ToString()),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            return claims;
        }

        public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
