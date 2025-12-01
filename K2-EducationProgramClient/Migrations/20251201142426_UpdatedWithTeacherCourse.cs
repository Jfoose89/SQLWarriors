using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWithTeacherCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_FkEnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_FkTeacherID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Courses_FkCourseID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teachers_FkTeacherID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FkCourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FkTeacherID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Grades_FkEnrollmentID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_FkTeacherID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Schedules",
                newName: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "StudentStatus",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Students",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ActiveTo",
                table: "Courses",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "CourseStatus",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeacherCourses",
                columns: table => new
                {
                    TeacherCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkTeacherID = table.Column<int>(type: "int", nullable: false),
                    FkCourseID = table.Column<int>(type: "int", nullable: false),
                    FkScheduleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses", x => x.TeacherCourseID);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Courses_FkCourseID",
                        column: x => x.FkCourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Schedules_FkScheduleID",
                        column: x => x.FkScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID");
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Teachers_FkTeacherID",
                        column: x => x.FkTeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseID",
                table: "Schedules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherID",
                table: "Schedules",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TeacherID",
                table: "Rooms",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherID",
                table: "Grades",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_FkCourseID",
                table: "TeacherCourses",
                column: "FkCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_FkScheduleID",
                table: "TeacherCourses",
                column: "FkScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_FkTeacherID",
                table: "TeacherCourses",
                column: "FkTeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Teachers_TeacherID",
                table: "Rooms",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Courses_CourseID",
                table: "Schedules",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Teachers_TeacherID",
                table: "Schedules",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Teachers_TeacherID",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Courses_CourseID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teachers_TeacherID",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "TeacherCourses");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TeacherID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EnrollmentID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_TeacherID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "EnrollmentID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CourseStatus",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Schedules",
                newName: "StartDate");

            migrationBuilder.AlterColumn<string>(
                name: "StudentStatus",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Students",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Schedules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ActiveTo",
                table: "Courses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FkCourseID",
                table: "Schedules",
                column: "FkCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FkTeacherID",
                table: "Schedules",
                column: "FkTeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FkEnrollmentID",
                table: "Grades",
                column: "FkEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FkTeacherID",
                table: "Grades",
                column: "FkTeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_FkEnrollmentID",
                table: "Grades",
                column: "FkEnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_FkTeacherID",
                table: "Grades",
                column: "FkTeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Courses_FkCourseID",
                table: "Schedules",
                column: "FkCourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Teachers_FkTeacherID",
                table: "Schedules",
                column: "FkTeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
