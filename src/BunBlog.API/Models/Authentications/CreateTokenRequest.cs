using Newtonsoft.Json;

namespace BunBlog.API.Models.Authentications
{
    /// <summary>
    /// 获取 token 的请求
    /// </summary>
    public class CreateTokenRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// password or refresh_token
        /// </summary>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Refresh token（使用时，grant_type 必须设为 refresh_token）
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
