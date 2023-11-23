using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class modyfied_blog_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Blogs");

            migrationBuilder.AddColumn<long>(
                name: "ImagenId",
                table: "Blogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ImagenId",
                table: "Blogs",
                column: "ImagenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Images_ImagenId",
                table: "Blogs",
                column: "ImagenId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Images_ImagenId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ImagenId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImagenId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Blogs",
                type: "longtext",
                nullable: false);
        }
    }
}
