using BunBlog.Core.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Settings
{
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Code).IsRequired();

            builder.HasIndex(c => c.Id).HasName("IX_Setting_Id");
            builder.HasIndex(c => c.Code).HasName("IX_Setting_Code").IsUnique();
        }
    }
}
