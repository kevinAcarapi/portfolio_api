using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_portfolio.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Technology_used = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Softskills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Soft_skill = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softskills", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Technical_skill = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "technologiesCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Project_technology = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_technologiesCatalog", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: false),
                    Profesion = table.Column<string>(type: "longtext", nullable: false),
                    Gmail = table.Column<string>(type: "longtext", nullable: false),
                    Curriculum = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Profile_photo = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects_TechnologiesCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProjectsId = table.Column<long>(type: "bigint", nullable: true),
                    TechnologiesCatalogId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects_TechnologiesCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_TechnologiesCatalog_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_TechnologiesCatalog_technologiesCatalog_Technologie~",
                        column: x => x.TechnologiesCatalogId,
                        principalTable: "technologiesCatalog",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cards",
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
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User_TechnicalSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsersId = table.Column<long>(type: "bigint", nullable: true),
                    TechnologysId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_TechnicalSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_TechnicalSkill_Technologies_TechnologysId",
                        column: x => x.TechnologysId,
                        principalTable: "Technologies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_TechnicalSkill_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users_SoftSkills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsersId = table.Column<long>(type: "bigint", nullable: true),
                    SoftSkillsId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_SoftSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_SoftSkills_Softskills_SoftSkillsId",
                        column: x => x.SoftSkillsId,
                        principalTable: "Softskills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_SoftSkills_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TechnologiesCatalog_ProjectsId",
                table: "Projects_TechnologiesCatalog",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TechnologiesCatalog_TechnologiesCatalogId",
                table: "Projects_TechnologiesCatalog",
                column: "TechnologiesCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TechnicalSkill_TechnologysId",
                table: "User_TechnicalSkill",
                column: "TechnologysId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TechnicalSkill_UsersId",
                table: "User_TechnicalSkill",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SoftSkills_SoftSkillsId",
                table: "Users_SoftSkills",
                column: "SoftSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SoftSkills_UsersId",
                table: "Users_SoftSkills",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Projects_TechnologiesCatalog");

            migrationBuilder.DropTable(
                name: "User_TechnicalSkill");

            migrationBuilder.DropTable(
                name: "Users_SoftSkills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "technologiesCatalog");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Softskills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
