using Bun.Blog.Core.Enums;

namespace Bun.Blog.Web.Admin.Models.Posts
{
    public class PostListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public PostStatus Status { get; set; }
    }
}
