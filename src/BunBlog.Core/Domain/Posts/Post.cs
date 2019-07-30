using BunBlog.Core.Domain.Categories;
using BunBlog.Core.Domain.Tags;
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
        /// 发表时间
        /// </summary>
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// 分类 Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 标签 Id
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public ulong Visits { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
