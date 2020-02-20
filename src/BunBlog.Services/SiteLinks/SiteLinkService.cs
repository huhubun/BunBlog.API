using BunBlog.Core.Domain.SiteLinks;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.SiteLinks
{
    public class SiteLinkService : ISiteLinkService
    {
        private readonly BunBlogContext _bunBlogContext;

        public SiteLinkService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<SiteLink>> GetListAsync()
        {
            return await _bunBlogContext.SiteLink
                                .OrderBy(l => l.Id)
                                .AsNoTracking()
                                .ToListAsync();
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

            return siteLink;
        }

        public async Task<SiteLink> EditAsync(SiteLink siteLink)
        {
            _bunBlogContext.Entry(siteLink).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return siteLink;
        }

        public async Task DeleteAsync(SiteLink siteLink)
        {
            _bunBlogContext.SiteLink.Remove(siteLink);
            await _bunBlogContext.SaveChangesAsync();
        }

    }
}
