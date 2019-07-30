using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Models.Authentication
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
