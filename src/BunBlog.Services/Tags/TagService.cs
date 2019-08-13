using BunBlog.Core.Domain.Tags;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly BunBlogContext _bunBlogContext;

        public TagService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<Tag>> GetListAsync()
        {
            return await _bunBlogContext.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<IsTagExists> IsExistsAsync(Tag tag)
        {
            if(await _bunBlogContext.Tags.AnyAsync(t => t.DisplayName == tag.DisplayName))
            {
                return IsTagExists.DisplayName;
            }

            if(await _bunBlogContext.Tags.AnyAsync(t => t.LinkName == tag.LinkName))
            {
                return IsTagExists.LinkName;
            }

            return IsTagExists.None;
        }

        public async Task<List<Tag>> GetListByLinkNameAsync(bool tracking = false, params string[] linkNames)
        {
            var tags = _bunBlogContext.Tags.Where(t => linkNames.Contains(t.LinkName));

            if (!tracking)
            {
                tags = tags.AsNoTracking();
            }

            return await tags.ToListAsync();
        }

        public async Task<Tag> GetByLinkNameAsync(string linkName, bool tracking = false)
        {
            var tag = _bunBlogContext.Tags.Where(t => t.LinkName == linkName);

            if (!tracking)
            {
                tag = tag.AsNoTracking();
            }

            return await tag.SingleOrDefaultAsync();
        }

        public async Task<bool> IsInUse(int id)
        {
            return await _bunBlogContext.PostTags.AnyAsync(t => t.TagId == id);
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            _bunBlogContext.Tags.Add(tag);
            await _bunBlogContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> EditAsync(Tag tag)
        {
            _bunBlogContext.Entry(tag).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return tag;
        }

        public async Task DeleteAsync(Tag tag)
        {
            _bunBlogContext.Tags.Remove(tag);
            await _bunBlogContext.SaveChangesAsync();
        }
    }
}
