using BunBlog.Core.Domain.Categories;
using BunBlog.Core.Domain.Configurations;
using BunBlog.Core.Domain.Images;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace BunBlog.Data
{
    public class BunBlogContext : DbContext
    {
        public BunBlogContext(DbContextOptions<BunBlogContext> options) : base(options)
        {
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

        public DbSet<Configuration> Configuration { get; set; }
    }
}
