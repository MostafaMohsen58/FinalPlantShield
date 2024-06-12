using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantShield.Data.Migrations
{
    public partial class RenameSomeColumnAndSomeDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Irrigation");

            migrationBuilder.RenameColumn(
                name: "IsAutomaticOrMannual",
                table: "Irrigation",
                newName: "IsAutomatic");

            migrationBuilder.AddColumn<bool>(
                name: "PumpState",
                table: "Irrigation",
                type: "bit",
                maxLength: 10,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Other_Treatment",
                table: "Disease",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PumpState",
                table: "Irrigation");

            migrationBuilder.DropColumn(
                name: "Other_Treatment",
                table: "Disease");

            migrationBuilder.RenameColumn(
                name: "IsAutomatic",
                table: "Irrigation",
                newName: "IsAutomaticOrMannual");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Irrigation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
