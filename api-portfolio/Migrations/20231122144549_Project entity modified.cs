using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class Projectentitymodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Projects");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ImageId",
                table: "Projects",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_ImageId",
                table: "Projects",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_ImageId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ImageId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Projects",
                type: "longtext",
                nullable: false);
        }
    }
}
