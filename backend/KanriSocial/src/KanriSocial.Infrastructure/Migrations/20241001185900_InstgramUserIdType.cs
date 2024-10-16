using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanriSocial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InstgramUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstagramUsers_AspNetUsers_UserId",
                table: "InstagramUsers");

            migrationBuilder.DropIndex(
                name: "IX_InstagramUsers_UserId",
                table: "InstagramUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "InstagramUsers",
                type: "uuid USING \"UserId\"::uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "InstagramUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstagramUsers_UserId1",
                table: "InstagramUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramUsers_AspNetUsers_UserId1",
                table: "InstagramUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstagramUsers_AspNetUsers_UserId1",
                table: "InstagramUsers");

            migrationBuilder.DropIndex(
                name: "IX_InstagramUsers_UserId1",
                table: "InstagramUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "InstagramUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InstagramUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_InstagramUsers_UserId",
                table: "InstagramUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramUsers_AspNetUsers_UserId",
                table: "InstagramUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
