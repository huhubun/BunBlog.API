using BunBlog.Core.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public interface IPostService
    {
        Task<List<Post>> GetListAsync();

        Task<Post> GetByIdAsync(int id, bool noTracking = true);

        Task<Post> PostAsync(Post post);
    }
}
