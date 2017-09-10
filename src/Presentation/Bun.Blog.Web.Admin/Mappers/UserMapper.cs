using AutoMapper;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Web.Admin.Models.Accounts;

namespace Bun.Blog.Web.Admin.Mappers
{
    public static class UserMapper
    {
        internal static IMapper Mapper { get; }

        static UserMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserMapperProfile>()).CreateMapper();
        }

        public static UserModel ToModel(this User entity)
        {
            return Mapper.Map<UserModel>(entity);
        }
    }
}
