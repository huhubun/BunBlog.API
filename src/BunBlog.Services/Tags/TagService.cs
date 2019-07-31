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

        public async Task<Tag> GetByLinkNameAsync(string linkName, bool noTracking = true)
        {
            var tag = _bunBlogContext.Tags.Where(t => t.LinkName == linkName);

            if (noTracking)
            {
                tag = tag.AsNoTracking();
            }

            return await tag.SingleOrDefaultAsync();
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
    }
}
