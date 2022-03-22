using BunBlog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Posts
{
    public class PostMetadataMap : IEntityTypeConfiguration<PostMetadata>
    {
        public void Configure(EntityTypeBuilder<PostMetadata> builder)
        {
            builder.ToTable("PostMetadata");

            builder.HasKey(pm => new { pm.PostId, pm.Key });

            builder.Property(pm => pm.PostId).IsRequired();
            builder.Property(pm => pm.Key).IsRequired();

            builder.HasIndex(pm => new { pm.PostId, pm.Key }).HasDatabaseName("IX_PostMetadata_PostId_Key");
        }
    }
}
