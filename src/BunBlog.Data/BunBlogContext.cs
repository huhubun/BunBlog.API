﻿using BunBlog.Core.Domain.Categories;
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

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
