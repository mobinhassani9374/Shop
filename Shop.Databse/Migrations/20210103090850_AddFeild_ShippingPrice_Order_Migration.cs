using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class AddFeild_ShippingPrice_Order_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingPrice",
                table: "Orders");
        }
    }
}
