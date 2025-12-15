USE EducationProgramDB;
GO

CREATE PROCEDURE dbo.StudentReportByPeriod
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		s.StudentID,
		s.FirstName + ' ' + s.LastName AS StudentName,
		s.Email,
		s.StudentStatus,
		s.StartDate,
		s.EndDate,
		COUNT(e.FKCourseID) AS CourseCount
	FROM dbo.Students s
	LEFT JOIN dbo.Enrollments e
		ON s.StudentID = e.FKStudentID
	GROUP BY
		s.StudentID,
		s.FirstName,
		s.LastName,
		s.Email,
		s.StudentStatus,
		s.StartDate,
		s.EndDate
	ORDER BY
		StudentName;
END
