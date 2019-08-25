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
    }
}
