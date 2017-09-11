using Bun.Blog.WebApi.Models.Users;
using System;

namespace Bun.Blog.WebApi.Models.Posts
{
    public class PostDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public UserOverviewModel Author { get; set; }
    }
}
