using BunBlog.Core.Consts;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BunBlog.API.Extensions
{
    public static class CacheEntryExtension
    {
        /// <summary>
        /// 将缓存失效规则设置为滑动，30天
        /// </summary>
        /// <param name="entry"></param>
        public static void SetSlidingExpirationByDefault(this ICacheEntry entry)
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(30 * Duration.DAY);
        }
    }
}
