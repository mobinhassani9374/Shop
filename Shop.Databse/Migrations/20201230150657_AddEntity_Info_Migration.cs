using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class AddEntity_Info_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Infoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstagramAccount = table.Column<string>(maxLength: 1000, nullable: true),
                    TelegramChanal = table.Column<string>(maxLength: 1000, nullable: true),
                    PhoneNumber1 = table.Column<string>(maxLength: 1000, nullable: true),
                    PhoneNumber2 = table.Column<string>(maxLength: 1000, nullable: true),
                    PhoneNumber3 = table.Column<string>(maxLength: 1000, nullable: true),
                    PhoneNumber4 = table.Column<string>(maxLength: 1000, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AboutUs = table.Column<string>(nullable: true),
                    LawUs = table.Column<string>(nullable: true),
                    HelpForBuy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infoes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Infoes");
        }
    }
}
