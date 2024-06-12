using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantShield.Data.Migrations
{
    public partial class AddRelationbetweenIrrigationAndStationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StationId",
                table: "Irrigation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Irrigation_StationId",
                table: "Irrigation",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Irrigation_Station_StationId",
                table: "Irrigation",
                column: "StationId",
                principalTable: "Station",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Irrigation_Station_StationId",
                table: "Irrigation");

            migrationBuilder.DropIndex(
                name: "IX_Irrigation_StationId",
                table: "Irrigation");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Irrigation");
        }
    }
}
