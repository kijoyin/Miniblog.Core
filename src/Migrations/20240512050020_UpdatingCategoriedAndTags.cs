using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miniblog.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingCategoriedAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categories",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categories",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "tags",
                table: "posts");
        }
    }
}
