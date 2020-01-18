using BunBlog.Core.Enums;
using System.Collections.Generic;

namespace BunBlog.API.Models.Posts
{
    /// <summary>
    /// 修改博文的请求
    /// </summary>
    public class EditBlogPostModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 链接名称
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// 分类（值为分类链接名称）
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 标签集合（值为标签链接名称）
        /// </summary>
        public List<string> TagList { get; set; } = new List<string>();
    }
}
