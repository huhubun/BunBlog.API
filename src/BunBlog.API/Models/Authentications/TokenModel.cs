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
    }
}
