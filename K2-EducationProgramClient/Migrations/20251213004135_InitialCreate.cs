using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "FK_Schedules_Courses_CourseID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Teachers_TeacherID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherCourses_Schedules_FkScheduleID",
                table: "TeacherCourses");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "FkScheduleID",
                table: "TeacherCourses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "ActiveFrom", "ActiveTo", "CourseName", "CourseStatus" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 1), null, "Mathematics 1", "Active" },
                    { 2, new DateOnly(2025, 1, 1), null, "Programming C#", "Active" },
                    { 3, new DateOnly(2025, 1, 1), null, "Swedish Literature", "Active" },
                    { 4, new DateOnly(2025, 1, 1), null, "World Geography", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomID", "Capacity", "RoomName", "TeacherID" },
                values: new object[,]
                {
                    { 1, 30, "A101", null },
                    { 2, 25, "B202", null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentID", "Email", "EndDate", "FirstName", "LastName", "StartDate", "StudentStatus" },
                values: new object[,]
                {
                    { 1, "adchariya.changtam@example.com", null, "Adchariya", "Changtam", new DateOnly(1, 1, 1), null },
                    { 2, "coday.awahmed@example.com", null, "Coday", "Awahmed", new DateOnly(1, 1, 1), null },
                    { 3, "hande.bengu@example.com", null, "Hande", "Bengu", new DateOnly(1, 1, 1), null },
                    { 4, "jordan.foose@example.com", null, "Jordan", "Foose", new DateOnly(1, 1, 1), null }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherID", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "eva.eriksson@example.com", "Eva", "Eriksson" },
                    { 2, "david.dahl@example.com", "David", "Dahl" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentID", "EnrollmentDate", "FkCourseID", "FkStudentID" },
                values: new object[,]
                {
                    { 1, new DateOnly(1, 1, 1), 1, 1 },
                    { 2, new DateOnly(1, 1, 1), 2, 2 },
                    { 3, new DateOnly(1, 1, 1), 1, 3 },
                    { 4, new DateOnly(1, 1, 1), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleID", "Date", "EndTime", "FkCourseID", "FkRoomID", "FkTeacherID", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 12, 15), new DateTime(2025, 12, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, new DateTime(2025, 12, 15, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateOnly(2025, 12, 16), new DateTime(2025, 12, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2, new DateTime(2025, 12, 16, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeID", "EnrollmentID", "FkEnrollmentID", "FkTeacherID", "GradeDate", "GradeValue", "TeacherID" },
                values: new object[,]
                {
                    { 1, null, 1, 1, new DateOnly(2025, 12, 15), "A", null },
                    { 2, null, 2, 2, new DateOnly(2025, 12, 16), "B", null },
                    { 3, null, 3, 2, new DateOnly(2025, 12, 17), "C", null },
                    { 4, null, 4, 1, new DateOnly(2025, 12, 18), "B", null }
                });

            migrationBuilder.InsertData(
                table: "TeacherCourses",
                columns: new[] { "TeacherCourseID", "FkCourseID", "FkScheduleID", "FkTeacherID" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_FkEnrollmentID",
                table: "Grades",
                column: "FkEnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades",
                column: "TeacherID",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_FkEnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherID",
                table: "Grades");

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
                name: "IX_Schedules_FkCourseID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FkTeacherID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Grades_FkEnrollmentID",
                table: "Grades");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumn: "TeacherCourseID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeacherCourses",
                keyColumn: "TeacherCourseID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherID",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "FkScheduleID",
                table: "TeacherCourses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseID",
                table: "Schedules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherID",
                table: "Schedules",
                column: "TeacherID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourses_Schedules_FkScheduleID",
                table: "TeacherCourses",
                column: "FkScheduleID",
                principalTable: "Schedules",
                principalColumn: "ScheduleID");
        }
    }
}
