using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bun.Blog.Data.Migrations
{
    public partial class AddPostMetaEntityAndPostWithCategoryRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Posts",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    InsertDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InsertUser = table.Column<string>(type: "text", nullable: true),
                    MetaKey = table.Column<string>(type: "text", nullable: true),
                    MetaValue = table.Column<string>(type: "text", nullable: true),
                    PostId = table.Column<int>(type: "int4", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdateUser = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMeta_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostMeta_PostId",
                table: "PostMeta",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Category_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Category_CategoryId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostMeta");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");
        }
    }
}
