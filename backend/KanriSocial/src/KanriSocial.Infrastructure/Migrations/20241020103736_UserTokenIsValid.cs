using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanriSocial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTokenIsValid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "UserTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "UserTokens");
        }
    }
}
