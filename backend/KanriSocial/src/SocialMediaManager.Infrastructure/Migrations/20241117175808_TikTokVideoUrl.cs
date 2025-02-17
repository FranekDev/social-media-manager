using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TikTokVideoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoSize",
                table: "TikTokVideos");

            migrationBuilder.AlterColumn<string>(
                name: "PublishId",
                table: "TikTokVideos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "TikTokVideos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "TikTokVideos");

            migrationBuilder.AlterColumn<string>(
                name: "PublishId",
                table: "TikTokVideos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoSize",
                table: "TikTokVideos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
