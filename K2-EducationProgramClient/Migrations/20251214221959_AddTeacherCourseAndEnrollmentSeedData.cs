using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherCourseAndEnrollmentSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Teachers_FkTeacherID",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Courses_FkCourseID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teachers_FkTeacherID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherCourses_Schedules_FkScheduleID",
                table: "TeacherCourses");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FkTeacherID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Grades_TeacherID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "FkScheduleID",
                table: "TeacherCourses");

            migrationBuilder.DropColumn(
                name: "FkCourseID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FkTeacherID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FkTeacherID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "EnrollmentID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Grades");

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 1,
                column: "FkTeacherCourseID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 2,
                column: "FkTeacherCourseID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FkTeacherCourseID",
                table: "Schedules",
                column: "FkTeacherCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FkTeacherID",
                table: "Grades",
                column: "FkTeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_FkTeacherID",
                table: "Grades",
                column: "FkTeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_TeacherCourses_FkTeacherCourseID",
                table: "Schedules",
                column: "FkTeacherCourseID",
                principalTable: "TeacherCourses",
                principalColumn: "TeacherCourseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_FkTeacherID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_TeacherCourses_FkTeacherCourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FkTeacherCourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Grades_FkTeacherID",
                table: "Grades");

            migrationBuilder.AddColumn<int>(
                name: "FkScheduleID",
                table: "TeacherCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FkCourseID",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkTeacherID",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkTeacherID",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentID",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 1,
                columns: new[] { "EnrollmentID", "TeacherID" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 2,
                columns: new[] { "EnrollmentID", "TeacherID" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 3,
                columns: new[] { "EnrollmentID", "TeacherID" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 4,
                columns: new[] { "EnrollmentID", "TeacherID" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1,
                column: "FkTeacherID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 2,
                column: "FkTeacherID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 1,
                columns: new[] { "FkCourseID", "FkTeacherCourseID", "FkTeacherID" },
                values: new object[] { null, 0, null });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 2,
                columns: new[] { "FkCourseID", "FkTeacherCourseID", "FkTeacherID" },
                values: new object[] { null, 0, null });

            migrationBuilder.UpdateData(
                table: "TeacherCourses",
                keyColumn: "TeacherCourseID",
                keyValue: 1,
                column: "FkScheduleID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TeacherCourses",
                keyColumn: "TeacherCourseID",
                keyValue: 2,
                column: "FkScheduleID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FkTeacherID",
                table: "Rooms",
                column: "FkTeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherID",
                table: "Grades",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Teachers_FkTeacherID",
                table: "Rooms",
                column: "FkTeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourses_Schedules_FkScheduleID",
                table: "TeacherCourses",
                column: "FkScheduleID",
                principalTable: "Schedules",
                principalColumn: "ScheduleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
