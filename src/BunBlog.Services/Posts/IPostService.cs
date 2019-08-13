using BunBlog.Core.Domain.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public interface IPostService
    {
        Task<List<Post>> GetListAsync(int pageIndex = 1, int pageSize = 10, bool orderByPublishedOnDesc = true);

        Task<List<Post>> GetListByTagAsync(int tagId, bool tracking = false);

        Task<Post> GetByIdAsync(int id, bool tracking = false);

        Task<Post> PostAsync(Post post);
    }
}
