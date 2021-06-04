using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class AddFeild_EducationFile_EducationId_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "EducationFiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EducationFiles_EducationId",
                table: "EducationFiles",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationFiles_Educations_EducationId",
                table: "EducationFiles",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationFiles_Educations_EducationId",
                table: "EducationFiles");

            migrationBuilder.DropIndex(
                name: "IX_EducationFiles_EducationId",
                table: "EducationFiles");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "EducationFiles");
        }
    }
}
