using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDataBaseImplement.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseYear",
                table: "EducationCourses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseYear",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseNum",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseYear",
                table: "EducationCourses");

            migrationBuilder.DropColumn(
                name: "CourseYear",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseNum",
                table: "Clients");
        }
    }
}
