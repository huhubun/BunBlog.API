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
            builder.Property(t => t.Name).IsRequired();

            builder.HasIndex(t => t.Id).HasName("IX_Tag_Id");
            builder.HasIndex(t => t.Name).IsUnique().HasName("IX_Tag_Name");
        }
    }
}
