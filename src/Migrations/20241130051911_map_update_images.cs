using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempoMapRepository.Migrations
{
    /// <inheritdoc />
    public partial class map_update_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageFormat",
                table: "Maps",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageFormat",
                table: "Maps");
        }
    }
}
