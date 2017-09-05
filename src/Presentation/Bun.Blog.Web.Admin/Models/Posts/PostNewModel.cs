using Bun.Blog.Core.Enums;

namespace Bun.Blog.Web.Admin.Models.Posts
{
    public class PostNewModel
    {
        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public PostStatus Status { get; set; }
    }
}
