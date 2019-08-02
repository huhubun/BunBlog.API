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
    }
}
