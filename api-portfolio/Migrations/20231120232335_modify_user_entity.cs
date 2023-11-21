using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class modify_user_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "longtext", nullable: false),
                    Url = table.Column<string>(type: "longtext", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageId",
                table: "Users",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Users_ImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Users",
                type: "longtext",
                nullable: false);
        }
    }
}
