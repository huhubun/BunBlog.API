using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.Services.Paging
{
    public class Paged<T> where T : new()
    {
        public static Paged<T> For(IQueryable<T> query, int pageIndex = 1, int pageSize = 10)
        {
            var skip = (pageIndex - 1) * pageSize;

            return new Paged<T>
            {
                Page = pageIndex,
                Size = pageSize,
                Total = query.Count(),
                Items = query.Skip(skip).Take(pageSize).ToList()
            };
        }

        public static async Task<Paged<T>> Async(IQueryable<T> query, int pageIndex = 1, int pageSize = 10)
        {
            var skip = (pageIndex - 1) * pageSize;

            return new Paged<T>
            {
                Page = pageIndex,
                Size = pageSize,
                Total = query.Count(),
                Items = await query.Skip(skip).Take(pageSize).ToListAsync()
            };
        }

        /// <summary>
        /// 当前页的数据
        /// </summary>
        public List<T> Items { get; private init; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; private init; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage => Total % Size == 0 ? Total / Size : ((Total / Size) + 1);

        /// <summary>
        /// 每页条目数
        /// </summary>
        public int Size { get; private init; }

        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total { get; private init; }
    }
}
