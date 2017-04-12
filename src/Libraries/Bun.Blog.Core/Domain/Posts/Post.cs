using Bun.Blog.Core.Data;
using Bun.Blog.Core.Domain.Users;

namespace Bun.Blog.Core.Domain.Posts
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int Status { get; set; }

        public User Author { get; set; }
    }
}
