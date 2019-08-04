using BunBlog.Core.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public interface IPostService
    {
        Task<List<Post>> GetListAsync(int pageIndex = 1, int pageSize = 10, bool orderByPublishedOnDesc = true);

        Task<Post> GetByIdAsync(int id, bool tracking = false);

        Task<Post> PostAsync(Post post);
    }
}
