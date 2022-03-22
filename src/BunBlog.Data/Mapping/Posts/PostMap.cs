using BunBlog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Posts
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.LinkName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PublishedOn).IsRequired();
            builder.Property(c => c.Type).IsRequired();

            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            builder.HasMany(p => p.TagList).WithOne(pt => pt.Post).HasForeignKey(pt => pt.PostId);
            builder.HasMany(p => p.MetadataList).WithOne().HasForeignKey(pt => pt.PostId);

            builder.HasIndex(c => c.Id).HasDatabaseName("IX_Post_Id");
            builder.HasIndex(c => new { c.LinkName, c.Type }).IsUnique().HasDatabaseName("IX_Post_LinkName_Type");
            builder.HasIndex(c => c.PublishedOn).HasDatabaseName("IX_Post_PublishedOn");
        }
    }
}
