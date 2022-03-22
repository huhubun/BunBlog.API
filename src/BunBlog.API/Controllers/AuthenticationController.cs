using BunBlog.API.Models.Authentications;
using BunBlog.Core.Consts;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IBunAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IBunAuthenticationService authenticationService,
            IMapper mapper)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _mapper = mapper;
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
        /// WRONG_USERNAME_OR_PASSWORD: 用户名或密码不匹配
        /// INVALID_GRANT_TYPE: 无效的 grant_type
        /// INVALID_REFRESH_TOKEN: 无效的 refresh_token
        /// </response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult CreateToken(CreateTokenRequest request)
        {
            _logger.LogInformation($"GrantType is [{request.GrantType}]");

            CreateTokenResult createTokenResult;

            switch (request.GrantType)
            {
                case GrantType.REFRESH_TOKEN:
                    createTokenResult = CreateTokenByRefreshToken(request);
                    break;

                case GrantType.PASSWORD:
                    createTokenResult = CreateTokenByPassword(request);
                    break;

                default:
                    createTokenResult = new CreateTokenResult(ErrorResponseCode.INVALID_GRANT_TYPE);
                    break;
            }

            if (createTokenResult.ErrorCode != null)
            {
                _logger.LogError($"ErrorCode is [{createTokenResult.ErrorCode}]");
                return HandlerCreateTokenError(createTokenResult);
            }

            _logger.LogInformation($"Created token successfully");

            return Ok(_mapper.Map<TokenModel>(createTokenResult));
        }

        [Authorize]
        [HttpGet("endsession")]
        public async Task<IActionResult> EndSession()
        {
            var idClaim = User.Claims.First(c => c.Type == JwtClaimTypes.Id);

            _authenticationService.EndSession(idClaim?.Value, Request.Headers["Authorization"]);

            return NoContent();
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);

            if (idClaim == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Username = idClaim.Value
            });
        }

        private CreateTokenResult CreateTokenByRefreshToken(CreateTokenRequest request)
        {
            return _authenticationService.CreateToken(Request.Headers["Authorization"], request.RefreshToken);
        }

        private CreateTokenResult CreateTokenByPassword(CreateTokenRequest request)
        {
            var user = _authenticationService.GetUser(request.Username, request.Password);

            if (user == null)
            {
                return new CreateTokenResult(ErrorResponseCode.WRONG_USERNAME_OR_PASSWORD);
            }

            return _authenticationService.CreateToken(user);
        }

        private IActionResult HandlerCreateTokenError(CreateTokenResult createTokenResult)
        {
            string errorMessage;

            switch (createTokenResult.ErrorCode)
            {
                case AuthenticationConst.INVALID_REFRESH_TOKEN:
                    errorMessage = "无效的 refresh_token";
                    break;

                case ErrorResponseCode.INVALID_GRANT_TYPE:
                    errorMessage = "无效的 grant_type";
                    break;

                case ErrorResponseCode.WRONG_USERNAME_OR_PASSWORD:
                    errorMessage = "用户名或密码不匹配";
                    break;

                default:
                    throw new Exception(createTokenResult.ErrorCode);
            }

            if (!String.IsNullOrEmpty(createTokenResult.ErrorMessage))
            {
                errorMessage = $"{errorMessage}：{createTokenResult.ErrorMessage}";
            }

            return BadRequest(new ErrorResponse(createTokenResult.ErrorCode, errorMessage));
        }
    }
}