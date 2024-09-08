using InventorySystem.Application;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helpers
{
    public class TokenUtil
    {
        private readonly JWTSettings _jwtSettings;
        public TokenUtil(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(LoginResponse response)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var expiresTime = _jwtSettings.ExpireInMinutes;
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[] {
                        new Claim("Role", response.Role.ToString()),
                        new Claim("Id", response.Id.ToString()),
                        new Claim("Warehouse", response.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiresTime),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
