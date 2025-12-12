using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K2_EducationProgramClient.Migrations
{
    /// <inheritdoc />
    public partial class Add_Database_Triggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // trg_Enrollment_StudentStatusCheck
            // Prevents enrollment if the student's status is 'Suspended', 'Inactive', 'Graduated', or 'Withdrawn'
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Enrollment_StudentStatusCheck
                ON Enrollments
                AFTER INSERT, UPDATE
                AS
                BEGIN
	                IF EXISTS (
		                SELECT 1
		                FROM inserted i
		                JOIN Students s ON i.FkStudentID = s.StudentID
		                WHERE s.StudentStatus IN ('Suspended', 'Inactive', 'Graduated', 'Withdrawn')
		                )
	                BEGIN
		                ROLLBACK TRANSACTION;
		                RAISERROR('Cannot create or update enrollment. The student status is not eligible for enrollment', 16, 1);
		                RETURN;
	                END
                END
            ");

            // trg_Schedule_ConflictCheck
            // Prevents scheduling a class if the room or the assigned teacher is already booked at that time
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Schedule_ConflictCheck
                ON Schedules
                AFTER INSERT, UPDATE
                AS
                BEGIN
	                DECLARE @ConflictFound INT = 0;

	                IF EXISTS (
		                SELECT 1
		                FROM inserted i
		                JOIN Schedules s ON s.FkRoomID = i.FkRoomID
					        AND s.Date = i.Date
					        AND s.ScheduleID != i.ScheduleID
		                WHERE i.StartTime < s.EndTime AND s.StartTime < i.EndTime
		                )
		                BEGIN
			                SET @ConflictFound = 1;
			                RAISERROR('Room is already booked for the selected date and time.', 16, 1);
		                END
	
		            IF @ConflictFound = 0 AND EXISTS (
			            SELECT 1
			            FROM inserted i
			            JOIN TeacherCourses current_tc ON current_tc.TeacherCourseID = i.FkTeacherCourseID
			            JOIN Schedules s ON s.ScheduleID != i.ScheduleID
			            JOIN TeacherCourses conflict_tc ON conflict_tc.TeacherCourseID = s.FkTeacherCourseID
			            
			            WHERE current_tc.FkTeacherID = conflict_tc.FkTeacherID
				            AND s.Date = i.Date
				            AND i.StartTime < s.EndTime AND s.StartTime < i.EndTime
			            )
		            BEGIN
			            SET @ConflictFound = 1
			            RAISERROR('Teacher is already scheduled for another class at the selected date and time.', 16, 1);
		            END
		            
		            IF @ConflictFound = 1
		            BEGIN
			            ROLLBACK TRANSACTION;
			            RETURN;
		            END
                END
            ");

            // TRG 3: trg_Schedule_CapacityCheck
            // Prevents scheduling if the number of enrolled students for the course exceeds the room's capacity
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Schedule_CapacityCheck
                ON Schedules
                AFTER INSERT, UPDATE
                AS
                BEGIN
	                IF EXISTS (
		                SELECT 1
		                FROM inserted i
		                JOIN Rooms r ON i.FkRoomID = r.RoomID
		                JOIN TeacherCourses tc ON i.FkTeacherCourseID = tc.TeacherCourseID
		                JOIN Courses c ON tc.FkCourseID = c.CourseID
		                JOIN Enrollments e ON c.CourseID = e.FkCourseID
		                GROUP BY i.ScheduleID, r.Capacity
		                HAVING COUNT (e.FkStudentID) > r.Capacity
		                )
	                BEGIN
		                ROLLBACK TRANSACTION;
		                RETURN;
	                END
                END
            ");

            // TRG 4: trg_Grade_ValidValueCheck
            // Ensures that the entered GradeValue is a valid letter grade ('A', 'B', 'C', 'D', or 'F').
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Grade_ValidValueCheck
                ON Grades
                AFTER INSERT, UPDATE
                AS
                BEGIN
	                IF EXISTS (
	                SELECT 1
	                FROM inserted i
	                WHERE i.GradeValue NOT IN ('A', 'B', 'C', 'D', 'F')
	                )
	                BEGIN
		                ROLLBACK TRANSACTION;
		                RAISERROR('The entered value for the grade must be ''A'', ''B'', ''C'', ''D'' or ''F''.', 16, 1);
		                RETURN;
	                END
                END
            ");

            // TRG 5: trg_Grade_EnrollmentCheck
            // Ensures that the FkEnrollmentID provided when creating a grade actually exists in the Enrollments table.
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Grade_EnrollmentCheck
                On Grades
                AFTER INSERT
                AS
                BEGIN
	                IF EXISTS (
		                SELECT 1
		                FROM inserted i
		                LEFT JOIN Enrollments e ON i.FkEnrollmentID= e.EnrollmentID
		                WHERE e.EnrollmentID IS NULL
		                )
		                BEGIN
			                ROLLBACK TRANSACTION;
			                RAISERROR('Cannot enter grade: The provided Enrollment ID does not exist in the system', 16, 1);
			                RETURN;
		                END
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Grade_EnrollmentCheck;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Grade_ValidValueCheck;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Schedule_CapacityCheck;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Schedule_ConflictCheck;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_Enrollment_StudentStatusCheck;");
        }
    }
}