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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        const int EXPIRES_IN_SECONDS = 1 * 60 * 60; // 3600s = 1h

        public AuthenticationController()
        {

        }

        [HttpPost("token")]
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