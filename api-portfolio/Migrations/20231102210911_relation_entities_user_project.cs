using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class relation_entities_user_project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Cards",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Cards");
        }
    }
}
