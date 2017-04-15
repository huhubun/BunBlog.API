using Bun.Blog.Core.Data;
using System;
using System.Reflection;

namespace Bun.Blog.Data.Extensions
{
    public static class SetBaseEntityExceptions
    {
        public static void SetInsertDate<T>(this T entity) where T : BaseEntity
        {
            typeof(T).GetProperty("InsertDate").SetValue(entity, DateTime.Now);
        }

        public static void SetUpdateDate<T>(this T entity) where T : BaseEntity
        {
            typeof(T).GetProperty("UpdateDate").SetValue(entity, DateTime.Now);
        }

        public static void SetInsertAndUpdateDate<T>(this T entity) where T : BaseEntity
        {
            SetInsertDate(entity);
            SetUpdateDate(entity);
        }
    }
}
