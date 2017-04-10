using System;

namespace Bun.Blog.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime InsertDate { get; set; }

        public string InsertUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateUser { get; set; }
    }
}
