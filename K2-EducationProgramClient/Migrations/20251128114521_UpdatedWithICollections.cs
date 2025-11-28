using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWithICollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeID",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GradeID",
                table: "Enrollments",
                column: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Grades_GradeID",
                table: "Enrollments",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "GradeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Grades_GradeID",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_GradeID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "GradeID",
                table: "Enrollments");
        }
    }
}
