using BunBlog.Core.Consts;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Enums;
using BunBlog.Data;
using BunBlog.Services.Paging;
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

        public async Task<Paged<Post>> GetListAsync(PostType? postType = PostType.Post, int pageIndex = 1, int pageSize = 10, bool orderByPublishedOnDesc = true)
        {
            var posts = _bunBlogContext.Posts
                            .Include(p => p.Category)
                            .Include(p => p.MetadataList)
                            .Include(p => p.TagList)
                                .ThenInclude(t => t.Tag)
                            .AsQueryable();

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

            return await Paged<Post>.Async(posts, pageIndex, pageSize);
        }

        public async Task<List<Post>> GetListByTagAsync(string tagLinkName)
        {
            var posts = await _bunBlogContext.Posts
                                    .Where(p => p.TagList.Any(t => t.Tag.LinkName == tagLinkName))
                                    .Include(p => p.Category)
                                    .Include(p => p.MetadataList)
                                    .Include(p => p.TagList)
                                        .ThenInclude(t => t.Tag)
                                    .AsNoTracking()
                                    .ToListAsync();

            return posts;
        }

        public async Task<List<Post>> GetListByCategoryAsync(string categoryLinkName)
        {
            var posts = await _bunBlogContext.Posts
                                    .Where(p => p.Category.LinkName == categoryLinkName)
                                    .Include(p => p.Category)
                                    .Include(p => p.MetadataList)
                                    .Include(p => p.TagList)
                                        .ThenInclude(t => t.Tag)
                                    .AsNoTracking()
                                    .ToListAsync();

            return posts;
        }

        public async Task<List<string>> GetLinkNameList()
        {
            return await _bunBlogContext.Posts.Select(p => p.LinkName).AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id, PostType? postType = null, bool tracking = false)
        {
            var posts = _bunBlogContext.Posts
                        .Include(p => p.Category)
                        .Include(p => p.MetadataList)
                        .Include(p => p.TagList)
                            .ThenInclude(t => t.Tag)
                        .Where(p => p.Id == id);

            if (postType.HasValue)
            {
                posts = posts.Where(p => p.Type == postType);
            }

            if (!tracking)
            {
                posts = posts.AsNoTracking();
            }

            return await posts.SingleOrDefaultAsync();
        }

        public async Task<Post> GetByLinkNameAsync(string linkName, PostType postType = PostType.Post, bool tracking = false)
        {
            var posts = _bunBlogContext.Posts
                    .Include(p => p.Category)
                    .Include(p => p.MetadataList)
                    .Include(p => p.TagList)
                        .ThenInclude(t => t.Tag)
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
            if (post.For.HasValue)
            {
                var posted = _bunBlogContext.Posts.Where(p => p.Type == PostType.Post).SingleOrDefault(p => p.Id == post.For);

                posted.For = null;

                _bunBlogContext.Posts.Update(posted);
            }

            _bunBlogContext.Posts.Remove(post);

            await _bunBlogContext.SaveChangesAsync();
        }
    }
}
