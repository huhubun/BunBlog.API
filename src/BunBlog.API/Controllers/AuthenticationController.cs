using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Authentications;
using BunBlog.Core.Consts;
using BunBlog.Services.Authentications;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 认证
    /// </summary>
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IBunAuthenticationService _authenticationService;

        public AuthenticationController(
            IBunAuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
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
            var user = _authenticationService.GetUser(request.Username, request.Password);

            if (user == null)
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.WRONG_USERNAME_OR_PASSWORD, "用户名或密码不匹配"));
            }

            var tokenString = _authenticationService.CreateToken(user);

            return Ok(new TokenModel
            {
                AccessToken = tokenString,
                ExpiresIn = AuthenticationConst.TOKEN_EXPIRES_IN_SECONDS
            });
        }
    }
}