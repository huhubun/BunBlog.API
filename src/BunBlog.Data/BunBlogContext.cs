using BunBlog.Core.Domain.Categories;
using BunBlog.Core.Domain.Images;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Domain.Settings;
using BunBlog.Core.Domain.SiteLinks;
using BunBlog.Core.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;

namespace BunBlog.Data
{
    public class BunBlogContext : DbContext
    {
        public BunBlogContext(DbContextOptions<BunBlogContext> options) : base(options)
        {
            // TODO: 暂时启用 npgsql 时间类型兼容性，后面版本再升级数据
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BunBlogContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostMetadata> PostMetadatas { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Setting> Setting { get; set; }

        public DbSet<SiteLink> SiteLink { get; set; }
    }
}
