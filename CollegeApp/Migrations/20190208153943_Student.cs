using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegeApp.Migrations
{
    public partial class Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_CourseID",
                table: "Student",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CourseID",
                table: "Student",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CourseID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CourseID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Student");
        }
    }
}
