using BunBlog.Core.Domain.SiteLinks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.SiteLinks
{
    public interface ISiteLinkService
    {
        Task<List<SiteLink>> GetListAsync();

        Task<SiteLink> GetByIdAsync(int id, bool tracking = false);

        Task<SiteLink> AddAsync(SiteLink siteLink);

        Task<SiteLink> EditAsync(SiteLink siteLink);

        Task DeleteAsync(SiteLink siteLink);
    }
}
