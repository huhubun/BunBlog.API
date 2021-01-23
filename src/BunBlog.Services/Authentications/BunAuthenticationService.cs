using BunBlog.Core.Configuration;
using BunBlog.Core.Consts;
using BunBlog.Services.Securities;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace BunBlog.Services.Authentications
{
    public class BunAuthenticationService : IBunAuthenticationService
    {
        private const string REFRESH_TOKEN_CACHE_KEY = "REFRESH_TOKEN:{0}";
        private const string END_SESSION_ACCESS_TOKEN_CACHE_KEY = "END_SESSION_ACCESS_TOKEN:{0}:{1}";

        private readonly ILogger<BunAuthenticationService> _logger;
        private readonly AuthenticationConfig _authenticationConfig;
        private readonly IMemoryCache _memoryCache;
        private readonly ISecurityService _securityService;

        public BunAuthenticationService(
            ILogger<BunAuthenticationService> logger,
            AuthenticationConfig authenticationConfig,
            IMemoryCache memoryCache,
            ISecurityService securityService
            )
        {
            _logger = logger;
            _authenticationConfig = authenticationConfig;
            _memoryCache = memoryCache;
            _securityService = securityService;
        }

        public AuthenticationUserConfig GetUser(string username, string password)
        {
            var passwordHash = _securityService.Sha256(password);

            return _authenticationConfig.Users.Where(u => u.Username == username)
                .Where(u => u.Password == passwordHash)
                .SingleOrDefault();
        }

        #region Create Token

        public CreateTokenResult CreateToken(AuthenticationUserConfig user)
        {
            return CreateTokenImpl(user.Username);
        }

        public CreateTokenResult CreateToken(string authorizationHeader, string refreshToken)
        {
            var authorizationHeaderValue = AuthenticationHeaderValue.Parse(authorizationHeader);

            string errorMessage;
            if (authorizationHeaderValue.Scheme == JwtBearerDefaults.AuthenticationScheme)
            {
                var tokenValidationParameters = JwtTokenHelper.CreateTokenValidationParameters(_authenticationConfig.Issuer, _authenticationConfig.Audience, _authenticationConfig.Secret);
                tokenValidationParameters.ValidateLifetime = false;

                string username;

                try
                {
                    new JwtSecurityTokenHandler().ValidateToken(authorizationHeaderValue.Parameter, tokenValidationParameters, out var token);
                    username = JwtTokenHelper.GetUserName(token);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "创建 refresh token 时，解析原 access token 失败");
                    return new CreateTokenResult(AuthenticationConst.INVALID_REFRESH_TOKEN, ex.Message);
                }

                if (IsRefreshTokenValid(username, refreshToken))
                {
                    return CreateTokenImpl(username);
                }
                else
                {
                    errorMessage = "refresh token 验证失败";
                }
            }
            else
            {
                errorMessage = $"非法的 Authentication Scheme：{authorizationHeaderValue.Scheme}";
            }

            return new CreateTokenResult(AuthenticationConst.INVALID_REFRESH_TOKEN, errorMessage);
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

            var refreshToken = StoreRefreshToken(username, CreateRefreshToken());

            return new CreateTokenResult
            {
                AccessToken = accessTokenString,
                ExpiresIn = AuthenticationConst.ACCESS_TOKEN_EXPIRES_IN_SECONDS,
                RefreshToken = refreshToken
            };
        }

        #endregion

        #region End Session

        public void EndSession(string username, string authorizationHeader)
        {
            var authorizationHeaderValue = AuthenticationHeaderValue.Parse(authorizationHeader);
            var tokenSignature = authorizationHeaderValue.Parameter.Split('.').Last();

            LetAccessTokenEndSession(username, tokenSignature);
            RemoveRefreshToken(username);
        }

        private void LetAccessTokenEndSession(string username, string tokenSignature)
        {
            _memoryCache.Set<bool?>(
                String.Format(END_SESSION_ACCESS_TOKEN_CACHE_KEY, username, tokenSignature),
                true,
                TimeSpan.FromSeconds(AuthenticationConst.ACCESS_TOKEN_EXPIRES_IN_SECONDS)
            );
        }

        public bool CheckAlreadyEndSessionAccessToken(string username, string tokenSignature)
        {
            var result = _memoryCache.Get<bool?>(String.Format(END_SESSION_ACCESS_TOKEN_CACHE_KEY, username, tokenSignature)) ?? false;
            return result;
        }

        #endregion

        #region Refresh Token

        private string GetRefreshTokenKey(string username)
        {
            return String.Format(REFRESH_TOKEN_CACHE_KEY, username);
        }

        private string StoreRefreshToken(string username, string refreshToken)
        {
            RemoveRefreshToken(username);
            return _memoryCache.Set(GetRefreshTokenKey(username), refreshToken, DateTime.Now.AddSeconds(AuthenticationConst.REFRESH_TOKEN_EXPIRES_IN_SECONDS));
        }

        private string CreateRefreshToken()
        {
            return $"{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", String.Empty);
        }

        private void RemoveRefreshToken(string username)
        {
            _memoryCache.Remove(GetRefreshTokenKey(username));
        }

        public bool IsRefreshTokenValid(string username, string refreshToken)
        {
            return _memoryCache.Get<string>(String.Format(REFRESH_TOKEN_CACHE_KEY, username)) == refreshToken;
        }

        #endregion

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
