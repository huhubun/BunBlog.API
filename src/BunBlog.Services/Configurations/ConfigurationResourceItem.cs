namespace BunBlog.Services.Configurations
{
    /// <summary>
    /// 配置项定义
    /// </summary>
    public class ConfigurationResourceItem
    {
        /// <summary>
        /// 配置项代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 配置项分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 配置项的类型
        /// </summary>
        public ConfigurationResourceType Type { get; set; }

        /// <summary>
        /// 配置项值的类型
        /// </summary>
        public ConfigurationResourceValueType ValueType { get; set; }

        /// <summary>
        /// 允许为 null
        /// </summary>
        public bool AllowNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 配置项描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 配置项类型
    /// </summary>
    public enum ConfigurationResourceType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text
    }

    /// <summary>
    /// 配置项值的类型
    /// </summary>
    public enum ConfigurationResourceValueType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String
    }
}
