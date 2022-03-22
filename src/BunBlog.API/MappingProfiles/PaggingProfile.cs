using BunBlog.API.Models.Paging;
using BunBlog.Services.Paging;

namespace BunBlog.API.MappingProfiles
{
    public class PaggingProfile : Profile
    {
        public PaggingProfile()
        {
            CreateMap(typeof(Paged<>), typeof(PagedModel<>));
        }
    }
}
