using K2_EducationProgramClient.Data;
using System;
using System.Collections.Generic;
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

        internal static void CreateEnrollment(EducationProgramClientDbContext? db, DateOnly inEnrollmentDate)
        {
            var enrollment = new Enrollment
            {
                EnrollmentDate = inEnrollmentDate
            };

            if (db is not null)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                Console.WriteLine($"Enrollment added: {inEnrollmentDate}");
            }
        }

        internal static void CreateRoom(EducationProgramClientDbContext? db, string inRoomName, int inRoomCapacity, string inRoomTeacher)
        {
            var room = new Room
            {
                RoomName = inRoomName,
                Capacity = inRoomCapacity,
                //Teacher = inRoomTeacher
            };

            if (db is not null)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                Console.WriteLine($"Teacher added: '{inRoomName}' | {inRoomCapacity}");
            }
        }

        internal static void CreateStudent(EducationProgramClientDbContext? db, string inStudentFirstName, string inStudentLastName, string inStudentEmail, DateOnly inStartDate, DateOnly inEndDate, string v)
        {
            var student = new Student
            {
                FirstName = inStudentFirstName,
                LastName = inStudentLastName,
                Email = inStudentEmail,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
                StudentStatus = "Active"
            };

            if (db is not null)
            {
                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine($"Student added: '{inStudentFirstName} {inStudentLastName}' | {inStudentEmail} | {DateOnly.FromDateTime(DateTime.Today)} | {DateOnly.FromDateTime(DateTime.Today.AddYears(2))} | 'Active'");
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
    }
}
