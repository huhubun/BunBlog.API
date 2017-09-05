using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Web.Admin.Models.Accounts;
using Bun.Blog.Web.Admin.Models.Posts;

namespace Bun.Blog.Web.Admin.Extensions
{
    public static class CreateBunBlogMapExtensions
    {
        public static void CreateBunBlogMap(this IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserModel>();

            config.CreateMap<Post, PostModel>();
            config.CreateMap<PostModel, Post>();

            config.CreateMap<PostNewModel, Post>();

            config.CreateMap<Post, EditPostModel>();
            config.CreateMap<EditPostModel, Post>();

        }
    }
}
