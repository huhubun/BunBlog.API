using System.Collections.Generic;

namespace BunBlog.API.Models.Paging
{
    public class PagedModel<T> where T : new()
    {
        /// <summary>
        /// 当前页的数据
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页条目数
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total { get; set; }
    }
}
