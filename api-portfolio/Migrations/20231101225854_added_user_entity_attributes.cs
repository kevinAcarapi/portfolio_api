using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class added_user_entity_attributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Technology",
                newName: "Technical_skill");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "User",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Curriculum",
                table: "User",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Gmail",
                table: "User",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "User",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Profesion",
                table: "User",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Imagen = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Enlace = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserId",
                table: "Card",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Curriculum",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Profesion",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Technical_skill",
                table: "Technology",
                newName: "Description");
        }
    }
}
