using Bun.Blog.Web.Admin.Models.Accounts;

namespace Bun.Blog.Web.Admin.Models.Posts
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int Status { get; set; }

        public UserModel Author { get; set; }
    }
}
