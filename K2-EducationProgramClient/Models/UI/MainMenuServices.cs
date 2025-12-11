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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
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

            if (db is not null)
            {
                var student = db.Students.FirstOrDefault(s => s.Email == studentEmail);
                DeleteStudent(db, student);
            }
        }

        internal static void DeleteStudent(EducationProgramClientDbContext? db, Student studentToRemove)
        {
            if (db is not null)
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

            if (db is not null)
            {
                var foundStudent = db.Students
                                    .Where(s => s.FirstName == inStudentName || s.LastName == inStudentName)
                                    .Select(s => $"({s.StudentID}) {s.FirstName} {s.LastName} | {s.Email}")
                                    .ToList();

                ConsolePrintHelper.AdminMenu("-", foundStudent);
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

        internal static List<String?> GetCoursesAsList(EducationProgramClientDbContext? db)
        {
            if (db is not null)
            {
                var courseData = db.Courses
                    .Select(c => $"({c.CourseID}) '{c.CourseName}' | {c.ActiveFrom}-{c.ActiveTo} | {c.CourseStatus}")
                    .ToList();

                return courseData;
            }

            return null;
        }

        internal static List<string> GetStudentsAsList(EducationProgramClientDbContext? db)
        {
            if (db is not null)
            {
                var studentsNames = db.Students
                    .Select(s => $"({s.StudentID}) {s.FirstName} {s.LastName} | {s.Email}")
                    .ToList();

                return studentsNames;
            }

            return null;
        }

        internal static List<string> GetTeachersAsList(EducationProgramClientDbContext? db)
        {
            if (db is not null)
            {
                var teachers = db.Teachers
                    .Select(t => $"({t.TeacherID}) {t.FirstName} {t.LastName} | {t.Email}")
                    .ToList();

                return teachers;
            }

            return null;
        }

        internal static List<string> GetRoomsAsList(EducationProgramClientDbContext? db)
        {
            if (db is not null)
            {
                var rooms = db.Rooms
                    .Select(r => $"({r.RoomID}) {r.RoomName} {r.Capacity} | {r.Teacher}")
                    .ToList();

                return rooms;
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
