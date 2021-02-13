using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDataBaseImplement.Migrations
{
    public partial class newmigra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserType = table.Column<string>(nullable: false),
                    BlockStatus = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    DateRegister = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Lecturer = table.Column<string>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(nullable: false),
                    YearED = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    CostED = table.Column<decimal>(nullable: false),
                    PayStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Educations_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationCourses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationID = table.Column<int>(nullable: true),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EducationCourses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationCourses_Educations_EducationID",
                        column: x => x.EducationID,
                        principalTable: "Educations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(nullable: false),
                    EducationID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    DatePay = table.Column<DateTime>(nullable: false),
                    SumPay = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pays_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pays_Educations_EducationID",
                        column: x => x.EducationID,
                        principalTable: "Educations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationCourses_CourseID",
                table: "EducationCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_EducationCourses_EducationID",
                table: "EducationCourses",
                column: "EducationID");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ClientID",
                table: "Educations",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_ClientID",
                table: "Pays",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_EducationID",
                table: "Pays",
                column: "EducationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationCourses");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
