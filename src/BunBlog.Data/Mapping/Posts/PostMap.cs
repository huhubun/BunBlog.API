using BunBlog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BunBlog.Data.Mapping.Posts
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.PublishedOn).IsRequired();
            builder.Property(c => c.Visits).IsRequired();

            builder.HasIndex(c => c.Id);

            builder.HasOne(p => p.Category).WithOne().HasForeignKey<Post>(p => p.CategoryId);
            builder.HasMany(p => p.PostTags).WithOne(pt => pt.Post).HasForeignKey(pt => pt.PostId);
        }
    }
}
