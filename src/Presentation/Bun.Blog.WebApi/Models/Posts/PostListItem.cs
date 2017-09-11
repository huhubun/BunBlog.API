using Bun.Blog.WebApi.Models.Users;

namespace Bun.Blog.WebApi.Models.Posts
{
    public class PostListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public UserOverviewModel Author { get; set; }
    }
}
