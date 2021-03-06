namespace BunBlog.Services.Settings
{
    /// <summary>
    /// 配置项定义
    /// </summary>
    public class SettingDefinition
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
        public SettingType Type { get; set; }

        /// <summary>
        /// 配置项值的类型
        /// </summary>
        public SettingValueType ValueType { get; set; }

        /// <summary>
        /// Object 类型的规范，当 ValueType 为 Object 时可用
        /// </summary>
        public string Schema { get; set; }

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
    public enum SettingType
    {
        /// <summary>
        /// 文本（适用于单行内容）
        /// </summary>
        Text,

        /// <summary>
        /// 文本域（适用于多行内容）
        /// </summary>
        Textarea,

        /// <summary>
        /// 对象
        /// </summary>
        Object,

        /// <summary>
        /// 列表
        /// </summary>
        List
    }

    /// <summary>
    /// 配置项值的类型
    /// </summary>
    public enum SettingValueType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,

        /// <summary>
        /// 对象
        /// </summary>
        Object
    }
}
