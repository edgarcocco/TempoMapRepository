using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempoMapRepository.Migrations
{
    /// <inheritdoc />
    public partial class add_map_ondelete_cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset");

            migrationBuilder.AddForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset",
                column: "e.MapId",
                principalTable: "Maps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset");

            migrationBuilder.AddForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset",
                column: "e.MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
