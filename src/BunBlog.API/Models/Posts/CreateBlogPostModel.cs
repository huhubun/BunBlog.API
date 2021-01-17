using BunBlog.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace BunBlog.API.Models.Posts
{
    /// <summary>
    /// 创建博文的请求
    /// </summary>
    public class CreateBlogPostModel
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
        [DisplayName("链接名称")]
        public string LinkName { get; set; }

        /// <summary>
        /// 分类（值为分类链接名称）
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 标签集合（值为标签链接名称）
        /// </summary>
        public List<string> TagList { get; set; }

        /// <summary>
        /// 博文类型
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// 关联对应的草稿或已发布的博文
        /// </summary>
        public int? For { get; set; }
    }
}
