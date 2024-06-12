using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantShield.Data.Migrations
{
    public partial class AddStationTableWithItsTwoRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StationId",
                table: "SensorData",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoilType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Station_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_StationId",
                table: "SensorData",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_UserId",
                table: "Station",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorData_Station_StationId",
                table: "SensorData",
                column: "StationId",
                principalTable: "Station",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorData_Station_StationId",
                table: "SensorData");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropIndex(
                name: "IX_SensorData_StationId",
                table: "SensorData");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "SensorData");
        }
    }
}
