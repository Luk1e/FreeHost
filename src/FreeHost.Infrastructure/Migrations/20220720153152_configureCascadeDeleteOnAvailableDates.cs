using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeHost.Infrastructure.Migrations
{
    public partial class configureCascadeDeleteOnAvailableDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableDates_Places_PlaceId",
                table: "AvailableDates");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableDates_Places_PlaceId",
                table: "AvailableDates",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableDates_Places_PlaceId",
                table: "AvailableDates");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableDates_Places_PlaceId",
                table: "AvailableDates",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }
    }
}
