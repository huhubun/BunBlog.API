using BunBlog.Core.Consts;
using BunBlog.Core.Domain.SiteLinks;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.SiteLinks
{
    public class SiteLinkService : ISiteLinkService
    {
        private readonly BunBlogContext _bunBlogContext;
        private readonly IMemoryCache _cache;

        public SiteLinkService(
            BunBlogContext bunBlogContext,
            IMemoryCache cache)
        {
            _bunBlogContext = bunBlogContext;
            _cache = cache;
        }

        public async Task<List<SiteLink>> GetListAsync()
        {
            return await _cache.GetOrCreateAsync(CacheKeys.ALL_SITE_LINKS, entry =>
            {
                return _bunBlogContext.SiteLink
                                .OrderBy(l => l.Id)
                                .AsNoTracking()
                                .ToListAsync();
            });
        }

        public async Task<SiteLink> GetByIdAsync(int id, bool tracking = false)
        {
            IQueryable<SiteLink> siteLinks = _bunBlogContext.SiteLink;

            if (!tracking)
            {
                siteLinks = siteLinks.AsNoTracking();
            }

            return await siteLinks.SingleOrDefaultAsync(l => l.Id == id);
        }


        public async Task<SiteLink> AddAsync(SiteLink siteLink)
        {
            _bunBlogContext.SiteLink.Add(siteLink);
            await _bunBlogContext.SaveChangesAsync();

            await UpdateCache();

            return siteLink;
        }

        public async Task<SiteLink> EditAsync(SiteLink siteLink)
        {
            _bunBlogContext.Entry(siteLink).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            await UpdateCache();

            return siteLink;
        }

        public async Task DeleteAsync(SiteLink siteLink)
        {
            _bunBlogContext.SiteLink.Remove(siteLink);
            await _bunBlogContext.SaveChangesAsync();

            await UpdateCache();
        }

        private async Task UpdateCache()
        {
            _cache.Remove(CacheKeys.ALL_SITE_LINKS);
            await GetListAsync();
        }
    }
}
