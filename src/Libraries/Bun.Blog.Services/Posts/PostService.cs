using System;
using System.Collections.Generic;
using System.Text;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Data;
using System.Linq;

namespace Bun.Blog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;

        public PostService(BlogContext context)
        {
            _context = context;
        }

        public IList<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public Post GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
