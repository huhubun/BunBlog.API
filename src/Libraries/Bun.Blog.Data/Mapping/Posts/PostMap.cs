using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bun.Blog.Data.Mapping.Posts
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Author).WithMany().HasForeignKey(t => t.AuthorId);
            builder.HasOne(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId);
            builder.HasMany(t => t.Metas).WithOne().HasForeignKey(t => t.PostId);
        }
    }
}
