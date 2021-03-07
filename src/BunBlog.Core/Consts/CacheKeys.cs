namespace BunBlog.Core.Consts
{
    public static class CacheKeys
    {
        private const string CACHE_PREFIX = "BLOG_CACHE.";

        /// <summary>
        /// 所有友情链接
        /// </summary>
        public const string ALL_SITE_LINKS = CACHE_PREFIX + "ALL_SITE_LINKS";

        /// <summary>
        /// 所有的配置项
        /// </summary>
        public const string ALL_SETTINGS = CACHE_PREFIX + "ALL_SETTINGS";

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
