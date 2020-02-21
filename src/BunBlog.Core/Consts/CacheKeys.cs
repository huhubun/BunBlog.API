namespace BunBlog.Core.Consts
{
    public static class CacheKeys
    {
        private const string CACHE_PREFIX = "BLOG_CACHE.";

        /// <summary>
        /// API 获取所有友情链接
        /// </summary>
        public const string API_GET_SITE_LINKS_LIST = CACHE_PREFIX + "API_GET_SITE_LINKS_LIST";

        /// <summary>
        /// API 通过 Code 获取设置项
        /// </summary>

        public const string API_GET_SETTINGS_BY_CODE = CACHE_PREFIX + "API_GET_SETTINGS.{0}";

        /// <summary>
        /// API 通过 Id 获取博文详情
        /// </summary>

        public const string API_GET_POST_BY_ID = CACHE_PREFIX + "API_GET_POST_ID.{0}";

        /// <summary>
        /// API 通过 Link Name 获取博文详情
        /// </summary>
        public const string API_GET_POST_BY_LINK_NAME = CACHE_PREFIX + "API_GET_POST_LINK_NAME.{0}";
    }
}
