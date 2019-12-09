using BunBlog.Core.Consts;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Enums;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BunBlogContext _bunBlogContext;

        public PostService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<Post>> GetListAsync(PostType? postType = PostType.Post, int pageIndex = 1, int pageSize = 10, bool orderByPublishedOnDesc = true)
        {
            var posts = _bunBlogContext.Posts.AsQueryable();

            if (postType.HasValue)
            {
                posts = posts.Where(p => p.Type == postType);
            }

            if (orderByPublishedOnDesc)
            {
                posts = posts.OrderByDescending(p => p.PublishedOn);
            }
            else
            {
                posts = posts.OrderBy(p => p.PublishedOn);
            }

            var skip = (pageIndex - 1) * pageSize;
            posts = posts.Skip(skip).Take(pageSize);

            return await posts.ToListAsync();
        }

        public async Task<List<Post>> GetListByTagAsync(int tagId, bool tracking = false)
        {
            var posts = _bunBlogContext.Posts.Where(p => p.TagList.Any(t => t.TagId == tagId));

            if (!tracking)
            {
                posts = posts.AsNoTracking();
            }

            return await posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id, bool tracking = false)
        {
            var posts = _bunBlogContext.Posts
                    .Where(p => p.Id == id);

            if (!tracking)
            {
                posts = posts.AsNoTracking();
            }

            return await posts.SingleOrDefaultAsync();
        }

        public async Task<Post> GetByLinkNameAsync(string linkName, PostType postType = PostType.Post, bool tracking = false)
        {
            var posts = _bunBlogContext.Posts
                    .Where(p => p.LinkName == linkName)
                    .Where(p => p.Type == postType);

            if (!tracking)
            {
                posts = posts.AsNoTracking();
            }

            return await posts.SingleOrDefaultAsync();
        }

        public async Task<Post> PostAsync(Post post)
        {
            post.PublishedOn = DateTime.Now;
            post.MetadataList = new List<PostMetadata>
            {
                new PostMetadata
                {
                    Key = PostMetadataKey.VISITS,
                    Value = PostMetadataService.DEFAULT_VISITS_VALUE
                }
            };

            _bunBlogContext.Posts.Add(post);

            await _bunBlogContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> EditAsync(Post post)
        {
            if (post.Type != PostType.Post)
            {
                throw new InvalidOperationException("只能修改博文类型为“正式发布”的博文，但当前修改的为“草稿”");
            }

            _bunBlogContext.Posts.Update(post);
            await _bunBlogContext.SaveChangesAsync();

            return post;
        }

        public async Task<bool> LinkNameExists(string linkName, PostType type)
        {
            return await _bunBlogContext.Posts.AnyAsync(p => p.LinkName == linkName && p.Type == type);
        }

        public async Task<Post> EditDraftAsync(Post post)
        {
            if (post.Type != PostType.Draft)
            {
                throw new InvalidOperationException("只能修改博文类型为“草稿”的博文，但当前修改的为“正式发布”");
            }

            _bunBlogContext.Posts.Update(post);
            await _bunBlogContext.SaveChangesAsync();

            return post;
        }

        public async Task DeleteDraft(Post post)
        {
            _bunBlogContext.Posts.Remove(post);
            await _bunBlogContext.SaveChangesAsync();
        }
    }
}
