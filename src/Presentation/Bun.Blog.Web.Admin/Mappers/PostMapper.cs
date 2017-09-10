using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Web.Admin.Models.Posts;

namespace Bun.Blog.Web.Admin.Mappers
{
    public static class PostMapper
    {
        internal static IMapper Mapper { get; }

        static PostMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserMapperProfile>()).CreateMapper();
        }

        public static PostModel ToModel(this Post entity)
        {
            return Mapper.Map<PostModel>(entity);
        }

        public static Post ToEntity(this PostModel model)
        {
            return Mapper.Map<Post>(model);
        }
    }
}
