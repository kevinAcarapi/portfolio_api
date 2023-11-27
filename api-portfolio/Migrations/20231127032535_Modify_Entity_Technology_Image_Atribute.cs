using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class Modify_Entity_Technology_Image_Atribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Technologies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_ImageId",
                table: "Technologies",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Images_ImageId",
                table: "Technologies",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Images_ImageId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_ImageId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Technologies");
        }
    }
}
