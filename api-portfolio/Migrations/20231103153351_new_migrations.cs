using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class new_migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologiesByUser_Technologies_TechnologyId",
                table: "TechnologiesByUser");

            migrationBuilder.AlterColumn<long>(
                name: "TechnologyId",
                table: "TechnologiesByUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologiesByUser_Technologies_TechnologyId",
                table: "TechnologiesByUser",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologiesByUser_Technologies_TechnologyId",
                table: "TechnologiesByUser");

            migrationBuilder.AlterColumn<long>(
                name: "TechnologyId",
                table: "TechnologiesByUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologiesByUser_Technologies_TechnologyId",
                table: "TechnologiesByUser",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id");
        }
    }
}
