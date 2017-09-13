using Bun.Blog.Core.Data;

namespace Bun.Blog.Core.Domain.Posts
{
    public class PostMeta : BaseEntity
    {
        public int PostId { get; set; }

        public string MetaKey { get; set; }

        public string MetaValue { get; set; }
    }
}
