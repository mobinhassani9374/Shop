using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class AddField_GarantyCondition_Info_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GarantyCondition",
                table: "Infoes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GarantyCondition",
                table: "Infoes");
        }
    }
}
