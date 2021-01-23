using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BunBlog.Services.Authentications
{
    public static class JwtTokenHelper
    {
        public static TokenValidationParameters CreateTokenValidationParameters(string issuer, string audience, string secret)
        {
            return new TokenValidationParameters
            {
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role,

                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),

                RequireExpirationTime = true,
                ValidateLifetime = true
            };
        }

        public static string GetUserName(SecurityToken securityToken)
        {
            return (string)((JwtSecurityToken)securityToken).Payload[JwtClaimTypes.Id];
        }
    }
}
