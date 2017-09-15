using Bun.Blog.WebApi.Models.Categories;
using Bun.Blog.WebApi.Models.Users;

namespace Bun.Blog.WebApi.Models.Posts
{
    public class PostListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public UserOverviewModel Author { get; set; }

        public CategoryModel Category { get; set; }
    }
}
