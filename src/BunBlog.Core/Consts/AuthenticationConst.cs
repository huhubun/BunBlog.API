using System;
using System.Collections.Generic;
using System.Text;

namespace BunBlog.Core.Consts
{
    public class AuthenticationConst
    {
        /// <summary>
        /// Token 有效期（单位：秒）
        /// </summary>
        public const int TOKEN_EXPIRES_IN_SECONDS = 1 * 60 * 60; // 3600s = 1h

    }
}
