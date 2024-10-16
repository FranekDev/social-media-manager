using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanriSocial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IgPostUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstagramUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstagramUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstagramPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstagramUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Caption = table.Column<string>(type: "character varying(2200)", maxLength: 2200, nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContainerId = table.Column<string>(type: "text", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstagramPosts_InstagramUsers_InstagramUserId",
                        column: x => x.InstagramUserId,
                        principalTable: "InstagramUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstagramPosts_InstagramUserId",
                table: "InstagramPosts",
                column: "InstagramUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstagramUsers_UserId",
                table: "InstagramUsers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstagramPosts");

            migrationBuilder.DropTable(
                name: "InstagramUsers");
        }
    }
}
