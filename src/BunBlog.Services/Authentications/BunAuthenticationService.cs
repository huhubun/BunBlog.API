using BunBlog.Core.Configuration;
using BunBlog.Core.Consts;
using IdentityModel;
using Microsoft.Extensions.Caching.Memory;
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
        private const string REFRESH_TOKEN_CACHE_KEY = "REFRESH_TOKEN:{0}";

        private readonly AuthenticationConfig _authenticationConfig;
        private readonly IMemoryCache _memoryCache;

        public BunAuthenticationService(
            AuthenticationConfig authenticationConfig,
            IMemoryCache memoryCache
            )
        {
            _authenticationConfig = authenticationConfig;
            _memoryCache = memoryCache;
        }

        public AuthenticationUserConfig GetUser(string username, string password)
        {
            return _authenticationConfig.Users.Where(u => u.Username == username)
                .Where(u => u.Password == password)
                .SingleOrDefault();
        }

        public bool IsRefreshTokenValid(string username, string refreshToken)
        {
            return _memoryCache.Get<string>(String.Format(REFRESH_TOKEN_CACHE_KEY, username)) == refreshToken;
        }

        public CreateTokenResult CreateToken(AuthenticationUserConfig user)
        {
            return CreateTokenImpl(user.Username);
        }

        public CreateTokenResult CreateToken(string username, string refreshToken)
        {
            if (IsRefreshTokenValid(username, refreshToken))
            {
                return CreateTokenImpl(username);
            }

            return new CreateTokenResult(AuthenticationConst.INVALID_REFRESH_TOKEN);
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

        private string StoreRefreshTokenCache(string username, string refreshToken)
        {
            var key = String.Format(REFRESH_TOKEN_CACHE_KEY, username);

            _memoryCache.Remove(key);

            return _memoryCache.Set(key, refreshToken, DateTime.Now.AddSeconds(AuthenticationConst.REFRESH_TOKEN_EXPIRES_IN_SECONDS));
        }

        private string CreateRefreshToken()
        {
            return $"{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", String.Empty);
        }

        private CreateTokenResult CreateTokenImpl(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_authenticationConfig.Secret);
            var notValidBefore = DateTime.UtcNow;
            var expirationTime = notValidBefore.AddSeconds(AuthenticationConst.ACCESS_TOKEN_EXPIRES_IN_SECONDS);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience, _authenticationConfig.Audience),
                    new Claim(JwtClaimTypes.Issuer, _authenticationConfig.Issuer),
                    new Claim(JwtClaimTypes.Id, username)
                }),
                NotBefore = notValidBefore,
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessTokenString = tokenHandler.WriteToken(accessToken);

            var refreshToken = StoreRefreshTokenCache(username, CreateRefreshToken());

            return new CreateTokenResult
            {
                AccessToken = accessTokenString,
                ExpiresIn = AuthenticationConst.ACCESS_TOKEN_EXPIRES_IN_SECONDS,
                RefreshToken = refreshToken
            };
        }
    }
}
