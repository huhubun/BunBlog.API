using BunBlog.Core.Domain.Categories;
using System;
using System.Collections.Generic;

namespace BunBlog.Core.Domain.Posts
{
    public class Post
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

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
        /// 发表时间
        /// </summary>
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// 分类 Id
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public virtual ICollection<PostTag> TagList { get; set; }

        /// <summary>
        /// 元数据集合
        /// </summary>
        public virtual ICollection<PostMetadata> MetadataList { get; set; }
    }
}
