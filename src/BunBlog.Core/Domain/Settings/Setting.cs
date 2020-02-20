namespace BunBlog.Core.Domain.Settings
{
    // 为了避免与命名空间重名，实体类先叫做 Setting 吧
    /// <summary>
    /// 博客配置
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置项代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 配置项值
        /// </summary>
        public string Value { get; set; }
    }
}
