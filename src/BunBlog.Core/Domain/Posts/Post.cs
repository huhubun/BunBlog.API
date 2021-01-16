using BunBlog.Core.Domain.Categories;
using BunBlog.Core.Enums;
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
        /// 博文类型
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// 上次更新时间
        /// </summary>
        public DateTime? LastModifyOn { get; set; }

        /// <summary>
        /// 关联的 Post
        /// 当前博文类型为 Post，并且 For 不为空，则指向当前博文对应的草稿
        /// 当前博文类型为 Draft，并且 For 不为空，则指向当前草稿对应的博文
        /// </summary>
        public int? For { get; set; }

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
