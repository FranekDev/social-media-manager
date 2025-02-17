using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FbFeedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacebookFeedPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PageId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "character varying(63206)", maxLength: 63206, nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FacebookUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PagegPostId = table.Column<string>(type: "text", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookFeedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacebookFeedPosts_FacebookUsers_FacebookUserId",
                        column: x => x.FacebookUserId,
                        principalTable: "FacebookUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookFeedPosts_FacebookUserId",
                table: "FacebookFeedPosts",
                column: "FacebookUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacebookFeedPosts");
        }
    }
}
