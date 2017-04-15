using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Web.Models.Accounts;
using Bun.Blog.Web.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Extensions
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

        }
    }
}
