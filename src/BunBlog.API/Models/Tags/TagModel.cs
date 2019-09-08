using System.ComponentModel;

namespace BunBlog.API.Models.Tags
{
    /// <summary>
    /// 标签
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// 链接名称
        /// </summary>
        [DisplayName("链接名称")]
        public string LinkName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string DisplayName { get; set; }
    }
}
