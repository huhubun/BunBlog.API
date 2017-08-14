namespace Bun.Blog.Core.Enums
{
    public enum PostStatus
    {
        None = 0x0000,

        /// <summary>
        /// 已发布
        /// </summary>
        Published = 0x0001,

        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0x0010
    }
}
