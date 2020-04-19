using BunBlog.Core.Consts;
using BunBlog.Core.Domain.Posts;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public class PostMetadataService : IPostMetadataService
    {
        public const string DEFAULT_VISITS_VALUE = "0";

        private readonly BunBlogContext _bunBlogContext;

        public PostMetadataService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<PostMetadata> GetByKeyAsync(int postId, string key, bool tracking = false)
        {
            var metadatas = _bunBlogContext.PostMetadatas.Where(pm => pm.PostId == postId).Where(pm => pm.Key == key);

            if (!tracking)
            {
                metadatas = metadatas.AsNoTracking();
            }

            return await metadatas.SingleOrDefaultAsync();
        }

        public async Task<List<PostMetadata>> GetAllByPostIdAsync(int postId, bool tracking = false)
        {
            var metadatas = _bunBlogContext.PostMetadatas.Where(pm => pm.PostId == postId);

            if (!tracking)
            {
                metadatas = metadatas.AsNoTracking();
            }

            return await metadatas.ToListAsync();
        }

        public async Task<PostMetadata> AddAsync(PostMetadata postMetadata)
        {
            _bunBlogContext.PostMetadatas.Add(postMetadata);
            await _bunBlogContext.SaveChangesAsync();

            return postMetadata;
        }

        public async Task<PostMetadata> EditAsync(PostMetadata postMetadata)
        {
            _bunBlogContext.Entry(postMetadata).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return postMetadata;
        }

        public async Task<PostMetadata> AddVisitsAsync(int postId)
        {
            var metadata = await GetByKeyAsync(postId, PostMetadataKey.VISITS, tracking: true);

            if (metadata != null)
            {
                var visits = Convert.ToInt64(metadata.Value) + 1;
                metadata.Value = visits.ToString();

                await EditAsync(metadata);
            }
            else
            {
                metadata = new PostMetadata
                {
                    PostId = postId,
                    Key = PostMetadataKey.VISITS,
                    Value = DEFAULT_VISITS_VALUE
                };

                await AddAsync(metadata);
            }

            return metadata;
        }
    }
}
