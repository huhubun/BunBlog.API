namespace BunBlog.Core.Consts
{
    public class AuthenticationConst
    {
        /// <summary>
        /// Access token 有效期（单位：秒）
        /// </summary>
        public const int ACCESS_TOKEN_EXPIRES_IN_SECONDS = 1 * 60 * 60; // h * min * s: 3600s = 1h

        /// <summary>
        /// Refresh token 有效期（单位：秒）
        /// </summary>
        public const int REFRESH_TOKEN_EXPIRES_IN_SECONDS = 30 * 24 * 60 * 60; // 2592000s = 30d

        /// <summary>
        /// 无效的 refresh_token
        /// </summary>
        public const string INVALID_REFRESH_TOKEN = nameof(INVALID_REFRESH_TOKEN);
    }
}
