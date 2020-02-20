using BunBlog.Services.Settings;

namespace BunBlog.API.Models.Settings
{
    public class SettingDefinitionWithValueModel
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

        /// <summary>
        /// 配置项的值
        /// </summary>
        public string Value { get; set; }
    }
}
