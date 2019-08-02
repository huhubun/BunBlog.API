using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Authentications;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 认证
    /// </summary>
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        const int EXPIRES_IN_SECONDS = 1 * 60 * 60; // 3600s = 1h

        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取 token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "username": "admin",
        ///         "password": "Pa$$w0rd"
        ///     }
        /// </remarks>
        /// <param name="request">获取 token 的请求</param>
        /// <returns></returns>
        /// <response code="200">成功获取 token</response>
        /// <response code="400">
        /// 获取 token 失败
        /// WRONG_USERNAME_OR_PASSWORD 用户名或密码不匹配
        /// </response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult CreateToken(CreateTokenRequest request)
        {
            var authenticationSecret = _configuration.GetValue<string>("Authentication:Secret");
            if (String.IsNullOrEmpty(authenticationSecret))
            {
                throw new ArgumentNullException("Secret", "未配置 Authentication:Secret");
            }

            var users = new List<UserModel>();
            _configuration.Bind("Authentication:Users", users);

            var isUserMatched = users.Where(u => u.Username == request.Username)
                .Where(u => u.Password == request.Password)
                .Any();

            if (!isUserMatched)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.WRONG_USERNAME_OR_PASSWORD, "用户名或密码不匹配"));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(authenticationSecret);
            var notValidBefore = DateTime.UtcNow;
            var expirationTime = notValidBefore.AddSeconds(EXPIRES_IN_SECONDS);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience, "api"),
                    new Claim(JwtClaimTypes.Issuer, "https://api.bun.dev"),
                    new Claim(JwtClaimTypes.Id, request.Username)
                }),
                NotBefore = notValidBefore,
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new TokenModel
            {
                AccessToken = tokenString,
                ExpiresIn = EXPIRES_IN_SECONDS
            });
        }
    }
}