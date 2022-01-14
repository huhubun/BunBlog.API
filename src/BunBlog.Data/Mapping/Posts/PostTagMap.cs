using BunBlog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Posts
{
    public class PostTagMap : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.ToTable("PostTag");

            builder.HasKey(pt => new { pt.PostId, pt.TagId });

            builder.HasOne(pt => pt.Post).WithMany(p => p.TagList).HasForeignKey(pt => pt.PostId);
            builder.HasOne(pt => pt.Tag).WithMany().HasForeignKey(pt => pt.TagId);

            builder.HasIndex(pt => pt.PostId).HasDatabaseName("IX_PostTag_PostId");
            builder.HasIndex(pt => pt.TagId).HasDatabaseName("IX_PostTag_TagId");
        }
    }
}
