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

        public static UserModel MapToModel(this User entity)
        {
            return Mapper.Map<UserModel>(entity);
        }

        #endregion

        #region User

        public static PostModel MapToModel(this Post entity)
        {
            return Mapper.Map<PostModel>(entity);
        }

        #endregion


    }
}
