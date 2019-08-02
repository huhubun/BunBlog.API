namespace BunBlog.API.Models.Authentications
{
    public class TokenModel
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 有效时长（单位：秒）
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Token 类型，返回固定值 "Bearer"
        /// </summary>
        public string TokenType => "Bearer";
    }
}
