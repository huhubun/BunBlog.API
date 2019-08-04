namespace BunBlog.Core.Domain.Posts
{
    public class PostMetadata
    {
        /// <summary>
        /// 博文 Id
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 元数据 Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 元数据值
        /// </summary>
        public string Value { get; set; }
    }
}
