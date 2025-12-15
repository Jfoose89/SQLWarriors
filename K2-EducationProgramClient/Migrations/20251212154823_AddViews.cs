using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class AddViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //StudentSummaryView --summary of each student, including their full name, status, total courses enrolled, and average grade
            migrationBuilder.Sql(@"
                CREATE VIEW StudentSummaryView AS
                SELECT
                    s.StudentID,
                    s.FirstName + ' ' + s.LastName AS StudentFullName, 
                    s.StudentStatus,
                    COUNT(e.EnrollmentID) AS TotalCoursesEnrolled,
                    AVG(CAST(
                        CASE g.GradeValue
                            WHEN 'A' THEN 4.0
                            WHEN 'B' THEN 3.0
                            WHEN 'C' THEN 2.0
                            WHEN 'D' THEN 1.0
                            ELSE 0.0
                        END 
                    AS DECIMAL(3, 2))) AS AverageGradeValue
                FROM Students s
                LEFT JOIN Enrollments e ON s.StudentID = e.FkStudentID
                LEFT JOIN Grades g ON e.EnrollmentID = g.FkEnrollmentID
                GROUP BY 
                    s.StudentID, 
                    s.FirstName, 
                    s.LastName, 
                    s.StudentStatus;
            ");

            //FailingCourseReportView --to identify all situations where a student got 'F' as a grade (failed), showing the student, course, and the teacher
            migrationBuilder.Sql(@"
                CREATE VIEW FailingCourseReportView AS
                SELECT DISTINCT
                    s.StudentID,
                    s.FirstName + ' ' + s.LastName AS StudentFullName,
                    c.CourseName,
                    t.FirstName + ' ' + t.LastName AS TeacherName, 
                    g.GradeValue AS FinalGrade
                FROM Students s
                LEFT JOIN Enrollments e ON s.StudentID = e.FkStudentID
                LEFT JOIN Courses c ON e.FkCourseID = c.CourseID
                LEFT JOIN Grades g ON e.EnrollmentID = g.FkEnrollmentID
                LEFT JOIN TeacherCourses tc ON c.CourseID = tc.FkCourseID
                LEFT JOIN Teachers t ON tc.FkTeacherID = t.TeacherID
                WHERE g.GradeValue = 'F';
            ");

            //CoursePerformanceView --to summarize course enrollment numbers and average grades
            migrationBuilder.Sql(@"
                CREATE VIEW CoursePerformanceView AS
                SELECT
                    c.CourseName,
                    COUNT(DISTINCT e.EnrollmentID) AS TotalEnrollments,
                    AVG(CAST(
                        CASE g.GradeValue
                            WHEN 'A' THEN 4.0
                            WHEN 'B' THEN 3.0
                            WHEN 'C' THEN 2.0
                            WHEN 'D' THEN 1.0
                            ELSE 0.0
                        END 
                    AS DECIMAL(3, 2))) AS AverageCourseGrade
                FROM Courses c
                LEFT JOIN Enrollments e ON c.CourseID = e.FkCourseID
                LEFT JOIN Grades g ON e.EnrollmentID = g.FkEnrollmentID
                
                GROUP BY 
                    c.CourseID, 
                    c.CourseName;
            ");

            //RoomUtilizationView  --how much time each room is booked for scheduled classes
            migrationBuilder.Sql(@"
                CREATE VIEW RoomUtilizationView AS
                SELECT
                    r.RoomName,
                    r.Capacity,
                    COUNT(sch.ScheduleID) AS TotalSchedules,
                    SUM(DATEDIFF(MINUTE, sch.StartTime, sch.EndTime)) AS TotalScheduledMinutes
                FROM Rooms r
                LEFT JOIN Schedules sch ON r.RoomID = sch.FkRoomID
                GROUP BY 
                    r.RoomID, 
                    r.RoomName, 
                    r.Capacity;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS StudentSummaryView;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS FailingCourseReportView;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS CoursePerformanceView;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS RoomUtilizationView;");
        }
    }
}