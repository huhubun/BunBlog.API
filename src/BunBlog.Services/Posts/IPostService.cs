using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Enums;
using BunBlog.Services.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public interface IPostService
    {
        Task<Paged<Post>> GetListAsync(PostType? postType = PostType.Post, int pageIndex = 1, int pageSize = 10, bool orderByPublishedOnDesc = true);

        Task<List<Post>> GetListByTagAsync(int tagId, bool tracking = false);

        Task<Post> GetByIdAsync(int id, bool tracking = false);

        Task<Post> GetByLinkNameAsync(string linkName, PostType postType = PostType.Post, bool tracking = false);

        Task<Post> PostAsync(Post post);

        Task<Post> EditAsync(Post post);

        Task<bool> LinkNameExists(string linkName, PostType type);

        Task<Post> EditDraftAsync(Post post);

        Task DeleteDraft(Post post);
    }
}
