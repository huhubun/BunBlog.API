using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BunBlog.API.Models.Authentication;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        public AuthenticationController()
        {

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
        /// <response code="401">获取 token 失败</response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public IActionResult CreateToken(CreateTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes("this_is_a_secret");
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