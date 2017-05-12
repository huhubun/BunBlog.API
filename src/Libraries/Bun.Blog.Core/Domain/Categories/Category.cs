using Bun.Blog.Core.Data;

namespace Bun.Blog.Core.Domain.Categories
{
    public class Category : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
