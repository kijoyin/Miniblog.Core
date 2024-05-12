using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miniblog.Core.Migrations
{
    /// <inheritdoc />
    public partial class FixingCommentsDto2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_commentdto_posts_postdtoid",
                table: "commentdto");

            migrationBuilder.DropPrimaryKey(
                name: "pk_commentdto",
                table: "commentdto");

            migrationBuilder.RenameTable(
                name: "commentdto",
                newName: "comments");

            migrationBuilder.RenameIndex(
                name: "ix_commentdto_postdtoid",
                table: "comments",
                newName: "ix_comments_postdtoid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_comments",
                table: "comments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_comments_posts_postdtoid",
                table: "comments",
                column: "postdtoid",
                principalTable: "posts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_posts_postdtoid",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_comments",
                table: "comments");

            migrationBuilder.RenameTable(
                name: "comments",
                newName: "commentdto");

            migrationBuilder.RenameIndex(
                name: "ix_comments_postdtoid",
                table: "commentdto",
                newName: "ix_commentdto_postdtoid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_commentdto",
                table: "commentdto",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_commentdto_posts_postdtoid",
                table: "commentdto",
                column: "postdtoid",
                principalTable: "posts",
                principalColumn: "id");
        }
    }
}
