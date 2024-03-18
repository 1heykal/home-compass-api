using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeCompassApi.Migrations
{
    /// <inheritdoc />
    public partial class NullablePropeities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable("Reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
