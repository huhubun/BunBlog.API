using Bun.Blog.Core.Enums;

namespace Bun.Blog.Web.Admin.Models.Posts
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public PostStatus Status { get; set; }

        public bool IsNew => Id == 0;
    }
}
