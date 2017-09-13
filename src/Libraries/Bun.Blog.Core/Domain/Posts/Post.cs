using Bun.Blog.Core.Data;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Core.Enums;
using System.Collections.Generic;

namespace Bun.Blog.Core.Domain.Posts
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public string Draft { get; set; }

        public string AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public PostStatus Status { get; set; }

        public virtual User Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<PostMeta> Metas { get; set; }
    }
}
