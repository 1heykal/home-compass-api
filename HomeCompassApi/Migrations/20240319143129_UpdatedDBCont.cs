using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeCompassApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBCont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Categories_CategoryId",
                table: "Facilities");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Categories_CategoryId",
                table: "Facilities",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Categories_CategoryId",
                table: "Facilities");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Categories_CategoryId",
                table: "Facilities",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
