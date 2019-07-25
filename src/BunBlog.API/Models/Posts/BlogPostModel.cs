using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Models.Posts
{
    public class BlogPostModel
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
        /// 发表时间
        /// </summary>
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public CategoryModel Category { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagModel> Tags { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public ulong Visits { get; set; }
    }
}
