using BunBlog.Core.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Tags
{
    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.LinkName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.DisplayName).IsRequired();

            builder.HasIndex(t => t.Id).HasDatabaseName("IX_Tag_Id");
            builder.HasIndex(t => t.LinkName).IsUnique().HasDatabaseName("IX_Tag_LinkName");
            builder.HasIndex(t => t.DisplayName).IsUnique().HasDatabaseName("IX_Tag_DisplayName");
        }
    }
}
