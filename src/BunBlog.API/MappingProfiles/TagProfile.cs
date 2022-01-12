using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Domain.Tags;

namespace BunBlog.API.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<PostTag, TagModel>().ConvertUsing((pt, tm, rc) =>
            {
                return rc.Mapper.Map<TagModel>(pt.Tag);
            });

            CreateMap<TagModel, Tag>();
        }
    }
}
