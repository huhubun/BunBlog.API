using BunBlog.API.Models.SiteLinks;
using BunBlog.Core.Domain.SiteLinks;

namespace BunBlog.API.MappingProfiles
{
    public class SiteLinkProfile : Profile
    {
        public SiteLinkProfile()
        {
            CreateMap<SiteLink, SiteLinkModel>();
            CreateMap<SiteLinkModel, SiteLink>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
