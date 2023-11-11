using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class Nuevasmigracionesparauser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftSkillByUser");

            migrationBuilder.DropTable(
                name: "TechnologiesByUser");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Technologies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SoftSkills",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_UserId",
                table: "Technologies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftSkills_UserId",
                table: "SoftSkills",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoftSkills_Users_UserId",
                table: "SoftSkills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Users_UserId",
                table: "Technologies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftSkills_Users_UserId",
                table: "SoftSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Users_UserId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_UserId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_SoftSkills_UserId",
                table: "SoftSkills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SoftSkills");

            migrationBuilder.CreateTable(
                name: "SoftSkillByUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SoftSkillId = table.Column<long>(type: "bigint", nullable: true),
                    UsersId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftSkillByUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftSkillByUser_SoftSkills_SoftSkillId",
                        column: x => x.SoftSkillId,
                        principalTable: "SoftSkills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoftSkillByUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TechnologiesByUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TechnologyId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologiesByUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologiesByUser_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologiesByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SoftSkillByUser_SoftSkillId",
                table: "SoftSkillByUser",
                column: "SoftSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftSkillByUser_UsersId",
                table: "SoftSkillByUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologiesByUser_TechnologyId",
                table: "TechnologiesByUser",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologiesByUser_UserId",
                table: "TechnologiesByUser",
                column: "UserId");
        }
    }
}
