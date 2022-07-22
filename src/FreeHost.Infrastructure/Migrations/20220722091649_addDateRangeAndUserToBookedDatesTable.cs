using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeHost.Infrastructure.Migrations
{
    public partial class addDateRangeAndUserToBookedDatesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "BookedDates",
                newName: "StartDate");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "BookedDates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BookedDates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BookedDates_ClientId",
                table: "BookedDates",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedDates_AspNetUsers_ClientId",
                table: "BookedDates",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedDates_AspNetUsers_ClientId",
                table: "BookedDates");

            migrationBuilder.DropIndex(
                name: "IX_BookedDates_ClientId",
                table: "BookedDates");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "BookedDates");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BookedDates");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "BookedDates",
                newName: "Date");
        }
    }
}
