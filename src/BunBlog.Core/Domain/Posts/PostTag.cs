using BunBlog.Core.Domain.Tags;

namespace BunBlog.Core.Domain.Posts
{
    public class PostTag
    {
        public int PostId { get; set; }

        public int TagId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
