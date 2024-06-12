using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantShield.Data.Migrations
{
    public partial class UpdateDiseaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Solution",
                table: "Disease",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solution",
                table: "Disease");
        }
    }
}
