using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                attachUserToContext(context, token);
            }

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                //UserConfiguration config = new UserConfiguration();
                UserRequest config = new UserRequest();
                config.Id = jwtToken.Claims.First(x => x.Type == "Id").Value != null ? Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "Id").Value) : 0;
                config.RoleId = jwtToken.Claims.First(x => x.Type == "Role").Value != null ? Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "Role").Value) : 0;
                config.Warehouse = jwtToken.Claims.First(x => x.Type == "Warehouse").Value != null ? Convert.ToInt32(jwtToken.Claims.First(x => x.Type == "Warehouse").Value) : 0;

                if (config.Id != 0)
                {
                    context.Items["UserConfig"] = config;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
