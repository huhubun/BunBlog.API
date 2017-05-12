using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bun.Blog.Data.Mapping.Categories
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}
