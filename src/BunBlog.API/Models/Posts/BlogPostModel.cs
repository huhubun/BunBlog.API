using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Enums;
using System;
using System.Collections.Generic;

namespace BunBlog.API.Models.Posts
{
    public class BlogPostModel
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
        /// 分类
        /// </summary>
        public CategoryModel Category { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagModel> TagList { get; set; }

        /// <summary>
        /// 元数据信息
        /// </summary>
        public List<PostMetadataModel> MetadataList { get; set; }

        /// <summary>
        /// 博文类型
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// 关联对应的草稿或已发布的博文
        /// </summary>
        public int? For { get; set; }

        /// <summary>
        /// 博文式样
        /// 用于存放博文样式相关的内容，内容为 JSON 格式，由前端自己解析
        /// </summary>
        public string Styling { get; set; }
    }
}
