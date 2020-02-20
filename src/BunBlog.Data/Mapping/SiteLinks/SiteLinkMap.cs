using BunBlog.Core.Domain.SiteLinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.SiteLinks
{
    public class SiteLinkMap : IEntityTypeConfiguration<SiteLink>
    {
        public void Configure(EntityTypeBuilder<SiteLink> builder)
        {
            builder.ToTable("SiteLink");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasIndex(c => c.Id).HasName("IX_SiteLink_Id");
        }
    }
}
