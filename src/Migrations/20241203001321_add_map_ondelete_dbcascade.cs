using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempoMapRepository.Migrations
{
    /// <inheritdoc />
    public partial class add_map_ondelete_dbcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset");

            migrationBuilder.DropForeignKey(
                name: "FK_Maps_AspNetUsers_e.UserId",
                table: "Maps");

            migrationBuilder.AddForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset",
                column: "e.MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maps_AspNetUsers_e.UserId",
                table: "Maps",
                column: "e.UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset");

            migrationBuilder.DropForeignKey(
                name: "FK_Maps_AspNetUsers_e.UserId",
                table: "Maps");

            migrationBuilder.AddForeignKey(
                name: "FK_MapDataset_Maps_e.MapId",
                table: "MapDataset",
                column: "e.MapId",
                principalTable: "Maps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maps_AspNetUsers_e.UserId",
                table: "Maps",
                column: "e.UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
