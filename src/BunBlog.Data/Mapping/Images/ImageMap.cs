using BunBlog.Core.Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Images
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Path).IsRequired();
            builder.Property(c => c.FileName).IsRequired();
            builder.Property(c => c.UploadTime).IsRequired();

            builder.HasIndex(c => c.Id).HasName("IX_Image_Id");
            builder.HasIndex(c => c.UploadTime).HasName("IX_Image_UploadTime");
        }
    }
}
