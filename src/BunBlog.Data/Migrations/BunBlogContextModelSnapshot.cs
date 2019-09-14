﻿// <auto-generated />
using System;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BunBlog.Data.Migrations
{
    [DbContext(typeof(BunBlogContext))]
    partial class BunBlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BunBlog.Core.Domain.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("LinkName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DisplayName")
                        .IsUnique()
                        .HasName("IX_Category_DisplayName");

                    b.HasIndex("Id")
                        .HasName("IX_Category_Id");

                    b.HasIndex("LinkName")
                        .IsUnique()
                        .HasName("IX_Category_LinkName");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Images.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<DateTime>("UploadTime");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("IX_Image_Id");

                    b.HasIndex("UploadTime")
                        .HasName("IX_Image_UploadTime");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<string>("Excerpt");

                    b.Property<string>("LinkName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("PublishedOn");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id")
                        .HasName("IX_Post_Id");

                    b.HasIndex("LinkName")
                        .IsUnique()
                        .HasName("IX_Post_LinkName");

                    b.HasIndex("PublishedOn")
                        .HasName("IX_Post_PublishedOn");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.PostMetadata", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("PostId", "Key");

                    b.HasIndex("PostId", "Key")
                        .HasName("IX_PostMetadata_PostId_Key");

                    b.ToTable("PostMetadata");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("PostId")
                        .HasName("IX_PostTag_PostId");

                    b.HasIndex("TagId")
                        .HasName("IX_PostTag_TagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("LinkName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DisplayName")
                        .IsUnique()
                        .HasName("IX_Tag_DisplayName");

                    b.HasIndex("Id")
                        .HasName("IX_Tag_Id");

                    b.HasIndex("LinkName")
                        .IsUnique()
                        .HasName("IX_Tag_LinkName");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.Post", b =>
                {
                    b.HasOne("BunBlog.Core.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.PostMetadata", b =>
                {
                    b.HasOne("BunBlog.Core.Domain.Posts.Post")
                        .WithMany("MetadataList")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BunBlog.Core.Domain.Posts.PostTag", b =>
                {
                    b.HasOne("BunBlog.Core.Domain.Posts.Post", "Post")
                        .WithMany("TagList")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BunBlog.Core.Domain.Tags.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
