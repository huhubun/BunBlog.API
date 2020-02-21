namespace BunBlog.Core.Consts
{
    /// <summary>
    /// 常用持续时长，单位为“秒”
    /// </summary>
    public static class Duration
    {
        /// <summary>
        /// 1 秒
        /// </summary>
        public const int SECOND = 1;

        /// <summary>
        /// 1 分钟（60 秒）
        /// </summary>
        public const int MINUTE = SECOND * 60;

        /// <summary>
        /// 1 小时（60 分钟）
        /// </summary>
        public const int HOUR = MINUTE * 60;

        /// <summary>
        /// 1 天（24 小时）
        /// </summary>
        public const int DAY = MINUTE * 24;

        /// <summary>
        /// 1 周（7 天）
        /// </summary>
        public const int WEEK = DAY * 7;

    }
}
