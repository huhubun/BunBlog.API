using BunBlog.Core.Configuration;
using BunBlog.Core.Consts;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BunBlog.Services.Authentications
{
    public class BunAuthenticationService : IBunAuthenticationService
    {
        private readonly AuthenticationConfig _authenticationConfig;

        public BunAuthenticationService(AuthenticationConfig authenticationConfig)
        {
            _authenticationConfig = authenticationConfig;
        }

        public AuthenticationUserConfig GetUser(string username, string password)
        {
            return _authenticationConfig.Users.Where(u => u.Username == username)
                .Where(u => u.Password == password)
                .SingleOrDefault();
        }

        public string CreateToken(AuthenticationUserConfig user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_authenticationConfig.Secret);
            var notValidBefore = DateTime.UtcNow;
            var expirationTime = notValidBefore.AddSeconds(AuthenticationConst.TOKEN_EXPIRES_IN_SECONDS);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience, _authenticationConfig.Audience),
                    new Claim(JwtClaimTypes.Issuer, _authenticationConfig.Issuer),
                    new Claim(JwtClaimTypes.Id, user.Username)
                }),
                NotBefore = notValidBefore,
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public static void AuthenticationConfigValidate(AuthenticationConfig authenticationConfig)
        {
            if (String.IsNullOrEmpty(authenticationConfig.Secret))
            {
                throw new ArgumentException("Secret not configured.", "Authentication:Secret");
            }

            if (String.IsNullOrEmpty(authenticationConfig.Issuer))
            {
                throw new ArgumentException("Issuer not configured.", "Authentication:Issuer");
            }

            if (String.IsNullOrEmpty(authenticationConfig.Audience))
            {
                throw new ArgumentException("Audience not configured.", "Authentication:Audience");
            }
        }
    }
}
