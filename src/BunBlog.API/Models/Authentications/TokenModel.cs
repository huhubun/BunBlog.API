using Newtonsoft.Json;

namespace BunBlog.API.Models.Authentications
{
    public class TokenModel
    {
        /// <summary>
        /// Access token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 有效时长（单位：秒）
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Token 类型，返回固定值 "Bearer"
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType => "Bearer";

        /// <summary>
        /// Refresh token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
