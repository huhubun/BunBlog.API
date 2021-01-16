using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BunBlog.Data.Migrations
{
    public partial class PostAddFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "For",
                table: "Post",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifyOn",
                table: "Post",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "For",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LastModifyOn",
                table: "Post");
        }
    }
}
