﻿using BunBlog.Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunBlog.Data.Mapping.Categories
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.LinkName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.DisplayName).IsRequired();

            builder.HasIndex(c => c.Id).HasDatabaseName("IX_Category_Id");
            builder.HasIndex(c => c.LinkName).IsUnique().HasDatabaseName("IX_Category_LinkName");
            builder.HasIndex(c => c.DisplayName).IsUnique().HasDatabaseName("IX_Category_DisplayName");
        }
    }
}
