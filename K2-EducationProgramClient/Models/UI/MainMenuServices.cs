using K2_EducationProgramClient.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models.UI
{
    public class MainMenuServices
    {
        internal static void CreateCourse(Data.EducationProgramClientDbContext? db, string inCourseName, string inCourseStatus, DateOnly inCourseStartDate, DateOnly inCourseEndDate)
        {
            var course = new Course
            {
                CourseName = inCourseName,
                CourseStatus = inCourseStatus,
                ActiveFrom = inCourseStartDate,
                ActiveTo = inCourseEndDate
            };

            if (db != null)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                Console.WriteLine($"Course added: '{inCourseName}' | {inCourseStartDate}-{inCourseEndDate} | {inCourseStatus}");
            }
        }

        internal static void CreateEnrollment(EducationProgramClientDbContext? db, int inCourseID, int inStudentID, DateOnly inEnrollmentDate)
        {
            var enrollment = new Enrollment
            {
                FkCourseID = inCourseID,
                FkStudentID = inStudentID,
                EnrollmentDate = inEnrollmentDate
            };

            if (db != null)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                Console.WriteLine($"Enrollment added: {inEnrollmentDate}");
            }
        }

        internal static void CreateGrade(EducationProgramClientDbContext db, int inEnrollmentID, int inTeacherID, DateOnly inGradeDate, string inGradeValue)
        {
            var grade = new Grade
            {
                FkEnrollmentID = inEnrollmentID,
                FkTeacherID = inTeacherID,
                GradeDate = inGradeDate,
                GradeValue = inGradeValue
            };

            if (db != null)
            {
                db.Grades.Add(grade);
                db.SaveChanges();
                Console.WriteLine($"Enrollment added: ({grade.GradeID})");
            }
        }

        internal static void CreateRoom(EducationProgramClientDbContext? db, string inRoomName, int inRoomCapacity, int inTeacherID)
        {
            var room = new Room
            {
                RoomName = inRoomName,
                Capacity = inRoomCapacity,
                FkTeacherID = inTeacherID
            };

            if (db != null)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                Console.WriteLine($"Teacher added: '{inRoomName}' | {inRoomCapacity}");
            }
        }

        internal static void CreateStudent(EducationProgramClientDbContext? db, string inStudentFirstName, string inStudentLastName, string inStudentEmail, DateOnly inStartDate, DateOnly inEndDate, string inStatus)
        {
            var student = new Student
            {
                FirstName = inStudentFirstName,
                LastName = inStudentLastName,
                Email = inStudentEmail,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
                StudentStatus = inStatus
            };

            if (db != null)
            {
                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine($"Student added: '{inStudentFirstName} {inStudentLastName}' | {inStudentEmail} | {DateOnly.FromDateTime(DateTime.Today)} | {DateOnly.FromDateTime(DateTime.Today.AddYears(2))} | 'Active'");
            }
        }
        internal static void CreateSchedule(EducationProgramClientDbContext? db, DateOnly inDate, int inCourseID, int inRoomID, DateTime inStartDateTime, DateTime inEndDateTime)
        {
            var schedule = new Schedule
            {
                Date = inDate,
                FkTeacherCourseID = inCourseID,
                FkRoomID = inRoomID,
                StartTime = inStartDateTime,
                EndTime = inEndDateTime
            };

            if (db != null)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                Console.WriteLine($"Schedule added: {inDate} | {inStartDateTime} - {inEndDateTime} | Room: {inRoomID} | Course: {inCourseID}");
            }
        }
        internal static void CreateTeacher(EducationProgramClientDbContext? db, string inTeacherFirstName, string inTeacherLastName, string inTeacherEmail)
        {
            var teacher = new Teacher
            {
                FirstName = inTeacherFirstName,
                LastName = inTeacherLastName,
                Email = inTeacherEmail
            };

            if (db != null)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                Console.WriteLine($"Teacher added: '{inTeacherFirstName} {inTeacherLastName}' | {inTeacherEmail}");
            }
        }
        internal static void CreateTeacherCourse(EducationProgramClientDbContext db, int inTeacherId, int inCourseID)
        {
            var teacherCourse = new TeacherCourse
            {
                FkTeacherID = inTeacherId,
                FkCourseID = inCourseID
            };

            if (db != null)
            {
                db.TeacherCourses.Add(teacherCourse);
                db.SaveChanges();
                Console.WriteLine($"Teacher course added: {inTeacherId} - {inCourseID}");
            }
        }

        internal static void DeleteStudentByEmail(EducationProgramClientDbContext? db, string studentEmail)
        {
            if (string.IsNullOrWhiteSpace(studentEmail))
            {
                ConsolePrintHelper.PrintError("Email cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            if (db != null)
            {
                var student = db.Students.FirstOrDefault(s => s.Email == studentEmail);
                DeleteStudent(db, student);
            }
        }

        internal static void DeleteStudent(EducationProgramClientDbContext? db, Student studentToRemove)
        {
            if (db != null)
            {
                Console.WriteLine($"Student removed: '{studentToRemove.FirstName} {studentToRemove.LastName}' | {studentToRemove.Email}");
                db.Students.Remove(studentToRemove);
                db.SaveChanges();
            }
        }

        internal static void FindStudentByNameAndPrintInfo(EducationProgramClientDbContext? db, string inStudentName)
        {
            if (string.IsNullOrWhiteSpace(inStudentName))
            {
                ConsolePrintHelper.PrintError("Email cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            if (db != null)
            {
                var foundStudent = db.Students
                                    .Where(s => s.FirstName == inStudentName || s.LastName == inStudentName)
                                    .Select(s => $"ID:{s.StudentID} | {s.FirstName} {s.LastName} | {s.Email}")
                                    .ToList();

                ConsolePrintHelper.AdminList("", foundStudent);
            }
        }

        internal static void RegisterStudentToCourseByID(EducationProgramClientDbContext? db, int inStudentID, int inCourseID, DateOnly inEnrollmentDate)
        {
            // Check if enrollment with student and course already exists
            var enrollmentExists = db.Enrollments.Any(e => e.FkStudentID == inStudentID && e.FkCourseID == inCourseID);
            if (enrollmentExists)
            {
                throw new DuplicateNameException("Student is already registered to this course.");
            }

            CreateEnrollment(db, inCourseID, inStudentID, inEnrollmentDate);
        }

        internal static List<string>? GetCoursesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var courseData = db.Courses
                    .Select(c => $"({c.CourseID}) '{c.CourseName}' | {c.ActiveFrom}-{c.ActiveTo} | {c.CourseStatus}")
                    .ToList();

                return courseData;
            }

            return null;
        }

        internal static List<string>? GetEnrollmentsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var enrollments = db.Enrollments.Select(e => $"({e.Student.StudentID}) {e.Student.FirstName} {e.Student.LastName} | {e.Course.CourseName} | {e.EnrollmentDate}").ToList();

                return enrollments;
            }

            return null;
        }

        internal static List<string>? GetGradesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var grades = db.Grades.Select(g => $"({g.GradeID}) {g.Enrollment.Student.FirstName} {g.Enrollment.Student.LastName} - [ {g.GradeValue}:{g.Enrollment.Course.CourseName} | {g.Teacher.FirstName} {g.Teacher.LastName}]").ToList();

                return grades;
            }

            return null;
        }

        internal static List<string>? GetRoomsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var rooms = db.Rooms
                    .Select(r => $"NR:{r.RoomID} - {r.RoomName} | Max Pers: {r.Capacity} | {r.Teacher.FirstName} {r.Teacher.LastName}")
                    .ToList();

                return rooms;
            }

            return null;
        }

        internal static List<string>? GetSchedulesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var schedules = db.Schedules.Select(s => $"{s.Date}|[{s.StartTime.TimeOfDay}] - [{s.EndTime.TimeOfDay}] | ({s.FkRoomID}) {s.Room.RoomName} | {s.TeacherCourse.Course.CourseName} - {s.TeacherCourse.Teacher.FirstName} {s.TeacherCourse.Teacher.LastName}").ToList();

                return schedules;
            }

            return null;
        }

        internal static List<string>? GetStudentsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var studentsNames = db.Students
                    .Select(s => $"ID:{s.StudentID} | NAME: {s.FirstName} {s.LastName} | EMAIL: {s.Email}")
                    .ToList();

                return studentsNames;
            }

            return null;
        }

        internal static List<string>? GetTeachersAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var teachers = db.Teachers
                    .Select(t => $"ID:{t.TeacherID} | NAME: {t.FirstName} {t.LastName} | EMAIL: {t.Email}")
                    .ToList();

                return teachers;
            }

            return null;
        }

        internal static List<string>? GetTeacherCoursesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var teacherCourses = db.TeacherCourses
                    .OrderBy(t => t.Course.CourseName)
                    .Select(t => $"{t.Course.CourseName} - {t.Teacher.FirstName} {t.Teacher.LastName}")
                    .ToList();

                return teacherCourses;
            }

            return null;
        }

        internal static List<string>? GetActiveCoursesWithRegisteredStudentsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var activeCourseWithStudents = db.Enrollments
                    .Where(e => e.Course.CourseStatus == "Active")
                    .OrderBy(e => e.Course.CourseName)
                    .ThenBy(e => e.Student.LastName)
                    .Select(e => $"{e.Course.CourseName} - {e.Student.FirstName} {e.Student.LastName} | {e.Student.Email}")
                    .ToList();

                return activeCourseWithStudents;
            }

            return null;
        }
        internal static List<string>? GetStudentCourseGradeTeacherList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var list = db.Grades
                    .Select(g => new
                    {
                        StudentName = g.Enrollment.Student.FirstName + " " + g.Enrollment.Student.LastName,
                        CourseName = g.Enrollment.Course.CourseName,
                        Grade = g.GradeValue,
                        GradeDate = g.GradeDate,
                        TeacherName = g.Teacher.FirstName + " " + g.Teacher.LastName
                    })
                    .ToList()
                    .Select(x => $"{x.StudentName} | {x.CourseName} | Grade: {x.Grade} | Date: {x.GradeDate} | Teacher: {x.TeacherName}")
                    .ToList();

                return list;
            }

            return null;
        }
        internal static List<Grade>? GetApprovedStudentsByTermList(EducationProgramClientDbContext? db, DateOnly startDate, DateOnly endDate)
        {
            if (db != null)
            {
                var obj = db.Grades
                    .Where(g => g.GradeValue != "F" && g.GradeDate >= startDate && g.GradeDate <= endDate)
                    .ToList();

                return obj;
            }

            return null;
        }

        internal static List<Grade>? GetNotApprovedStudentsByTermList(EducationProgramClientDbContext? db, DateOnly startDate, DateOnly endDate)
        {
            if (db != null)
            {
                var obj = db.Grades
                    .Where(g => g.GradeValue == "F" && g.GradeDate >= startDate && g.GradeDate <= endDate)
                    .ToList();

                return obj;
            }

            return null;
        }

        internal static List<string>? GetStudentApprovalReportByTermList(EducationProgramClientDbContext? db, DateOnly startDate, DateOnly endDate)
        {
            if (db != null)
            {
                var allStudents  = db.Grades
                        .Where(g => g.GradeDate >= startDate && g.GradeDate <= endDate)
                        .ToList();

                var approvedStudents = allStudents
                        .Where(g => g.GradeValue != "F")
                        .ToList();

                var aStudents = allStudents
                        .Where(g => g.GradeValue == "A")
                        .ToList();

                var bStudents = allStudents
                        .Where(g => g.GradeValue == "B")
                        .ToList();

                var cStudents = allStudents
                        .Where(g => g.GradeValue == "C")
                        .ToList();

                var dStudents = allStudents
                        .Where(g => g.GradeValue == "D")
                        .ToList();

                var fStudents = allStudents
                        .Where(g => g.GradeValue == "F")
                        .ToList();

                List<string> stringListToReturn = new List<string>
                {
                    $"[{startDate} - {endDate}]",
                    $"",
                    $" Approved: {approvedStudents.Count}",
                    $" Not Approved: {fStudents.Count}",
                    $"",
                    $" Total Grades:",
                    $"   - A: {aStudents.Count}",
                    $"   - B: {bStudents.Count}",
                    $"   - C: {cStudents.Count}",
                    $"   - D: {dStudents.Count}",
                    $"   - F: {fStudents.Count}"
                };

                return stringListToReturn;
            }

            return null;
        }

        internal static void ClearAndReseedTable(DbContext db, string tableName)
        {
            // Disable constraints (Risky?)
            db.Database.ExecuteSqlRaw($"ALTER TABLE [{tableName}] NOCHECK CONSTRAINT ALL;");
            // Delete all rows
            db.Database.ExecuteSqlRaw($"DELETE FROM [{tableName}];");
            // Reseed identity
            db.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('[{tableName}]', RESEED, 0);");
            // Re-enable constraints
            db.Database.ExecuteSqlRaw($"ALTER TABLE [{tableName}] CHECK CONSTRAINT ALL;");
        }
    }
}
