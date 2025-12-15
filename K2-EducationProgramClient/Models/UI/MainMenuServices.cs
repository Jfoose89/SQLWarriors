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
                Console.WriteLine($" Course added: '{inCourseName}' | {inCourseStartDate}-{inCourseEndDate} | {inCourseStatus}");
            }
        }

        internal static void CreateEnrollment(EducationProgramClientDbContext? db, int inCourseID, int inStudentID, DateOnly inEnrollmentDate)
        {
            // check if student and course exist 
            var findStudent = db.Students.FirstOrDefault(s => s.StudentID == inStudentID);
            var findCourse = db.Courses.FirstOrDefault(c => c.CourseID == inCourseID);

            // check if student is already enrolled in the course 
            var existingEnrollment = db.Enrollments.FirstOrDefault(e => e.FkCourseID == inCourseID && e.FkStudentID == inStudentID);
            if (existingEnrollment is not null)
            {
                Console.WriteLine(" This student is already enrolled in this course.");
                return;
            }

            // if student and course are exist create enrollment
            if (findStudent is not null && findCourse is not null)
            {
                var enrollment = new Enrollment
                {
                    FkStudentID = inStudentID,
                    EnrollmentDate = inEnrollmentDate,
                    FkCourseID = inCourseID
                };

                if (db is not null)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();

                    var course = db.Courses.FirstOrDefault(c => c.CourseID == inCourseID);
                    var student = db.Students.FirstOrDefault(s => s.StudentID == inStudentID);
                    Console.WriteLine($" Enrollment added: [{student.FirstName} {student.LastName} - {course.CourseName}]");
                }
            }
            else
            {
                Console.WriteLine($" Student or course not found");
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

                var enrollment = db.Enrollments.FirstOrDefault(e => e.EnrollmentID == inEnrollmentID);
                var teacher = db.Teachers.FirstOrDefault(t => t.TeacherID == inTeacherID);
                Console.WriteLine($" Grade added: [{enrollment.Course.CourseName} | {enrollment.Student.FirstName} {enrollment.Student.LastName} | {inGradeValue} | {inGradeDate.ToString("yyyy'/'MM'/'dd")} | {teacher.FirstName} {teacher.LastName}]");
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
                Console.WriteLine($" Teacher added: '{inRoomName}' | {inRoomCapacity}");
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
                Console.WriteLine($" Student added: '{inStudentFirstName} {inStudentLastName}' | {inStudentEmail} | {DateOnly.FromDateTime(DateTime.Today)} | {DateOnly.FromDateTime(DateTime.Today.AddYears(2))} | 'Active'");
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
                Console.WriteLine($" Schedule added: {inDate} | {inStartDateTime} - {inEndDateTime} | Room: {inRoomID} | Course: {inCourseID}");
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
                Console.WriteLine($" Teacher added: '{inTeacherFirstName} {inTeacherLastName}' | {inTeacherEmail}");
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

                var teacher = db.Teachers.FirstOrDefault(t => t.TeacherID == inTeacherId);
                var course = db.Courses.FirstOrDefault(c => c.CourseID == inCourseID);
                Console.WriteLine($" Teacher Course added: [¨{teacher.FirstName} {teacher.LastName} - {course.CourseName}]");
            }
        }

        internal static void DeleteStudentByEmail(EducationProgramClientDbContext? db, string studentEmail)
        {
            if (string.IsNullOrWhiteSpace(studentEmail))
            {
                ConsolePrintHelper.PrintError(" Email cannot be blank.");
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
                Console.WriteLine($" Student removed: '{studentToRemove.FirstName} {studentToRemove.LastName}' | {studentToRemove.Email}");
                db.Students.Remove(studentToRemove);
                db.SaveChanges();
            }
        }

        internal static void FindStudentByNameAndPrintInfo(EducationProgramClientDbContext? db, string inStudentName)
        {
            if (string.IsNullOrWhiteSpace(inStudentName))
            {
                ConsolePrintHelper.PrintError(" Email cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            if (db != null)
            {
                var foundStudent = db.Students
                                    .Where(s => s.FirstName == inStudentName || s.LastName == inStudentName)
                                    .Select(s => $"ID: {s.StudentID} | NAME: {s.FirstName} {s.LastName} | EMAIL: {s.Email}")
                                    .ToList();

                ConsolePrintHelper.AdminList("FOUND STUDENT", foundStudent);
            }
        }

        internal static void RegisterStudentToCourseByID(EducationProgramClientDbContext? db, int inStudentID, int inCourseID, DateOnly inEnrollmentDate)
        {
            // Check if enrollment with student and course already exists
            var enrollmentExists = db.Enrollments.Any(e => e.FkStudentID == inStudentID && e.FkCourseID == inCourseID);
            if (enrollmentExists)
            {
                throw new DuplicateNameException(" Student is already registered to this course.");
            }

            CreateEnrollment(db, inCourseID, inStudentID, inEnrollmentDate);
        }

        internal static List<string>? GetCoursesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var courseData = db.Courses
                    .Select(c => $" ID: {c.CourseID} | NAME: {c.CourseName} | ACTIVE TIME: {c.ActiveFrom.ToString("yyyy'/'MM'/'dd")} - {(c.ActiveTo.HasValue ? c.ActiveTo.Value.ToString("yyyy'/'MM'/'dd") : "N/A")} | STATUS: {c.CourseStatus}")
                    .ToList();

                return courseData;
            }

            return null;
        }

        internal static List<string>? GetEnrollmentsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var enrollments = db.Enrollments
                    .OrderBy(e => e.EnrollmentID)
                    .Select(e => $"ENROLLMENT ID:{e.EnrollmentID} | ENROLLMENT DATE:{e.EnrollmentDate.ToString("yyyy'/'MM'/'dd")} | STUDENT ID:{e.Student.StudentID} | STUDENT NAME:{e.Student.FirstName} {e.Student.LastName} | COURSE:{e.Course.CourseName}")
                    .ToList();

                return enrollments;
            }

            return null;
        }

        internal static List<string>? GetGradesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var grades = db.Grades.Select(g => $" ID:{g.GradeID} | STUDENT: {g.Enrollment.Student.FirstName} {g.Enrollment.Student.LastName} | GRADE: {g.GradeValue} | COURSE: {g.Enrollment.Course.CourseName} | TEACHER: {g.Teacher.FirstName} {g.Teacher.LastName}]").ToList();

                return grades;
            }

            return null;
        }

        internal static List<string>? GetRoomsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var rooms = db.Rooms
                    .Select(r => $" ROOM ID:{r.RoomID} | ROOM NAME: {r.RoomName} | ROOM CAPACITY: {r.Capacity} | TEACHER: {r.Teacher.FirstName} {r.Teacher.LastName}")
                    .ToList();

                return rooms;
            }

            return null;
        }

        internal static List<string>? GetSchedulesAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var schedules = db.Schedules.Select(s => $" DATE:{s.Date} | TIME: [{s.StartTime.TimeOfDay}] - [{s.EndTime.TimeOfDay}] | ROOM ID: {s.FkRoomID} | ROOM NAME: {s.Room.RoomName} | COURSE:{s.TeacherCourse.Course.CourseName} | TEACHER: {s.TeacherCourse.Teacher.FirstName} {s.TeacherCourse.Teacher.LastName}").ToList();

                return schedules;
            }

            return null;
        }

        internal static List<string>? GetStudentsAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var studentsNames = db.Students
                    .Select(s => $" ID:{s.StudentID} | NAME: {s.FirstName} {s.LastName} | EMAIL: {s.Email}")
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
                    .Select(t => $" ID:{t.TeacherID} | NAME: {t.FirstName} {t.LastName} | EMAIL: {t.Email}")
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
                    .Select(t => $" COURSE: {t.Course.CourseName} | TEACHER: {t.Teacher.FirstName} {t.Teacher.LastName}")
                    .ToList();
                return teacherCourses;
            }

            return null;
        }

        internal static List<string>? GetActiveCoursesWithRegisteredStudentsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var activeCourses = db.Enrollments
                                .Where(e => e.Course.CourseStatus == "Active")
                                .OrderBy(e => e.Course.CourseName)
                                .ThenBy(e => e.Student.LastName)
                                .Select(e => new
                                {
                                    CourseName = e.Course.CourseName,
                                    StudentName = e.Student.FirstName + " " + e.Student.LastName,
                                    Email = e.Student.Email
                                })
                                .ToList()
                                .GroupBy(x => x.CourseName)
                                .ToList();

                var listToReturn = new List<string>();

                foreach (var course in activeCourses)
                {
                    listToReturn.Add($"[ COURSE: {course.Key} ]");

                    int maxStudent = course.Max(x => x.StudentName.Length);
                    int maxEmail = course.Max(x => x.Email.Length);

                    foreach (var s in course)
                    {
                        listToReturn.Add(
                            $"   - STUDENT: {s.StudentName.PadRight(maxStudent)} | " +
                            $" EMAIL: {s.Email.PadRight(maxEmail)}"
                        );
                    }
                    listToReturn.Add("");
                }

                return listToReturn;
            }

            return null;
        }
        internal static List<string>? GetStudentCourseGradeTeacherList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var gradesList = db.Grades
                    .Select(g => new
                    {
                        StudentName = g.Enrollment.Student.FirstName + " " + g.Enrollment.Student.LastName,
                        CourseName = g.Enrollment.Course.CourseName,
                        Grade = g.GradeValue,
                        GradeDate = g.GradeDate,
                        TeacherName = g.Teacher.FirstName + " " + g.Teacher.LastName
                    })
                    .ToList();

                if (gradesList.Count() > 0)
                {
                    int maxStudent = gradesList.Max(x => x.StudentName.Length);
                    int maxCourse = gradesList.Max(x => x.CourseName.Length);
                    int maxGrade = gradesList.Max(x => x.Grade.Length);
                    int maxTeacher = gradesList.Max(x => x.TeacherName.Length);

                    var listToReturn = gradesList
                        .Select(x =>
                            $" [ STUDENT: {x.StudentName.PadRight(maxStudent)} ]\n" +
                            $"   COURSE : {x.CourseName.PadRight(maxCourse)}\n" +
                            $"   GRADE  : {x.Grade.PadRight(maxGrade)}\n" +
                            $"   DATE   : {x.GradeDate:yyyy/MM/dd}\n" +
                            $"   TEACHER: {x.TeacherName.PadRight(maxTeacher)}\n"
                        )
                        .ToList();

                    return listToReturn;
                }
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
                    $" [{startDate.ToString("yyyy'/'MM'/'dd")} - {endDate.ToString("yyyy'/'MM'/'dd")}]",
                    $"  --------------",
                    $"  Approved: {approvedStudents.Count}",
                    $"  Not Approved: {fStudents.Count}",
                    $"  --------------",
                    $"  All Grades",
                    $"  A: {aStudents.Count}",
                    $"  B: {bStudents.Count}",
                    $"  C: {cStudents.Count}",
                    $"  D: {dStudents.Count}",
                    $"  F: {fStudents.Count}",
                    $"  --------------"
                };

                return stringListToReturn;
            }

            return null;
        }

        internal static List<string>? GetCoursePerformanceAsList(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                var coursePerformance = db.CoursePerformances;


                List<string> stringListToReturn = new List<string>();

                foreach (var c in coursePerformance)
                {
                    // NOTE: Keep this formatting, weird in code but good in console.
                    stringListToReturn.Add(
                                    $@" [ {c.CourseName} ]
   - Total Enrollments: {c.TotalEnrollments}
   - Average Grade: {c.AverageCourseGrade.ToString("0.0")}
"
                
    );
                }
                

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
