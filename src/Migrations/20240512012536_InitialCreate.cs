using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miniblog.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    excerpt = table.Column<string>(type: "text", nullable: false),
                    ispublished = table.Column<bool>(type: "boolean", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pubdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "commentdto",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    author = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    isadmin = table.Column<bool>(type: "boolean", nullable: false),
                    pubdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    postdtoid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commentdto", x => x.id);
                    table.ForeignKey(
                        name: "fk_commentdto_posts_postdtoid",
                        column: x => x.postdtoid,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_commentdto_postdtoid",
                table: "commentdto",
                column: "postdtoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commentdto");

            migrationBuilder.DropTable(
                name: "posts");
        }
    }
}
