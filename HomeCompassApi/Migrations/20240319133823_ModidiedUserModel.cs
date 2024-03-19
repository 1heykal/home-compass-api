using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeCompassApi.Migrations
{
    /// <inheritdoc />
    public partial class ModidiedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_ApplicationUserId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ApplicationUserId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Resources");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ApplicationUserId",
                table: "Resources",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_ApplicationUserId",
                table: "Resources",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
