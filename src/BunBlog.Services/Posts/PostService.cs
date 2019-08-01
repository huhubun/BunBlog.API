using BunBlog.Core.Domain.Posts;
using BunBlog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BunBlog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BunBlogContext _bunBlogContext;

        public PostService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<Post>> GetListAsync()
        {
            return await _bunBlogContext.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id, bool noTracking = true)
        {
            var posts = _bunBlogContext.Posts.Where(p => p.Id == id);

            if (noTracking)
            {
                posts = posts.AsNoTracking();
            }

            return await posts.SingleOrDefaultAsync();
        }

        public async Task<Post> PostAsync(Post post)
        {
            post.PublishedOn = DateTime.Now;

            _bunBlogContext.Posts.Add(post);
            await _bunBlogContext.SaveChangesAsync();

            return post;
        }
    }
}
