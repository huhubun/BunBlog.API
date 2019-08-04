using BunBlog.Core.Domain.Posts;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public interface IPostMetadataService
    {
        Task<PostMetadata> GetByKeyAsync(int postId, string key, bool tracking = false);

        Task<PostMetadata> AddAsync(PostMetadata postMetadata);

        Task<PostMetadata> EditAsync(PostMetadata postMetadata);

        Task<PostMetadata> AddVisitsAsync(int postId);
    }
}
