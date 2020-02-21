using Microsoft.EntityFrameworkCore.Migrations;

namespace PTU_Tools_Web.Data.Migrations
{
    public partial class SheetName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SheetTitle",
                table: "Sheets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheetTitle",
                table: "Sheets");
        }
    }
}
