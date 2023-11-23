using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class modify_softSkill_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "SoftSkills",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoftSkills_ImageId",
                table: "SoftSkills",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoftSkills_Images_ImageId",
                table: "SoftSkills",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftSkills_Images_ImageId",
                table: "SoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_SoftSkills_ImageId",
                table: "SoftSkills");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "SoftSkills");
        }
    }
}
