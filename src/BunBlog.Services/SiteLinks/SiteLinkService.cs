using BunBlog.Core.Domain.SiteLinks;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BunBlog.Services.SiteLinks
{
    public class SiteLinkService : ISiteLinkService
    {
        private readonly BunBlogContext _bunBlogContext;

        public async Task<List<SiteLink>> GetListAsync()
        {
            return await _bunBlogContext.SiteLink.AsNoTracking().ToListAsync();
        }

    }
}
