using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Web.Models.Accounts;
using Bun.Blog.Web.Models.Posts;

namespace Bun.Blog.Web.Extensions
{
    public static partial class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #region User

        public static UserModel ToModel(this User entity)
        {
            return Mapper.Map<UserModel>(entity);
        }

        #endregion

        #region Post

        public static PostModel ToModel(this Post entity)
        {
            return Mapper.Map<PostModel>(entity);
        }

        #endregion

        #region PostNew

        public static Post ToEntity(this PostNewModel model)
        {
            return Mapper.Map<Post>(model);
        }

        #endregion

        #region EditPost

        public static Post ToEntity(this EditPostModel model, Post destination)
        {
            return Mapper.Map<EditPostModel, Post>(model, destination);
        }

        #endregion

    }
}
