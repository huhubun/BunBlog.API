using Bun.Blog.Web.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Models.Posts
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
