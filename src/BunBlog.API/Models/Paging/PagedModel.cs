using System.Collections.Generic;

namespace BunBlog.API.Models.Paging
{
    public class PagedModel<T> where T : new()
    {
        /// <summary>
        /// 当前页的数据
        /// </summary>
        public List<T> Items { get; private init; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Total { get; private init; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; private init; }

        /// <summary>
        /// 每页条目数
        /// </summary>
        public int Size { get; private init; }
    }
}
