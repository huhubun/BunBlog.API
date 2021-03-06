using Microsoft.EntityFrameworkCore.Migrations;

namespace BunBlog.Data.Migrations
{
    public partial class AddPostStyling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Styling",
                table: "Post",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Styling",
                table: "Post");
        }
    }
}
