using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeHost.Infrastructure.Migrations
{
    public partial class configureManyToManyOnAmenityPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Places_PlaceId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_PlaceId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Amenities");

            migrationBuilder.CreateTable(
                name: "AmenityPlace",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    PlacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityPlace", x => new { x.AmenitiesId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_AmenityPlace_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityPlace_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityPlace_PlacesId",
                table: "AmenityPlace",
                column: "PlacesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityPlace");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Amenities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_PlaceId",
                table: "Amenities",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Places_PlaceId",
                table: "Amenities",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }
    }
}
