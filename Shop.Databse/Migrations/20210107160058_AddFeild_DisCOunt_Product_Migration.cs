using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class AddFeild_DisCOunt_Product_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Discount",
                table: "Products",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsAmazing",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsAmazing",
                table: "Products");
        }
    }
}
