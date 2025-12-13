using K2_EducationProgramClient.Data;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models.UI
{
    public class MainMenu
    {
        public EducationProgramClientDbContext? db { get; set; }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("MAIN MENU", new List<string>
                {
                    "Add New Course",
                    "Add New Teacher",
                    "Add New Room",
                    "Add New Enrollment",
                    "Find Student and show [Courses, Grades, Teacher, GradeDate]",
                    "Add New Student",
                    "Remove Student",
                    "Edit Student",
                    "Show All Active Courses and Enrolled Students",
                    "Show All Students",
                    "Show Report of all approved and non approved student per term. (half year, full year and quarter year)"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": AddCourse(); break;
                    case "2": AddTeacher(); break;
                    case "3": AddRoom(); break;
                    case "4": AddEnrollment(); break;
                    case "5": FindStudent(); break;
                    case "6": AddStudent(); break;
                    case "7": RemoveStudent(); break;
                    case "8": EditStudentMenu(); break;
                    case "9": ShowActiveCoursesWithStudents(); break;
                    case "10": ShowAllStudents(); break;
                    case "11": ShowStudentsPerTerm(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }
        public void AddCourse()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new course");

            string? inCourseName = ConsolePrintHelper.AdminAskChoice("Enter Course Name: ");
            string? inCourseStatus = ConsolePrintHelper.AdminAskChoice("Enter Course Status: ");
            if (string.IsNullOrWhiteSpace(inCourseName) || string.IsNullOrWhiteSpace(inCourseStatus))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            DateOnly inCourseStartDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Course Start Date (YYYY-MM-DD): "), out inCourseStartDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Enter a date (yyyy-MM-dd): ");
            }
            
            DateOnly inCourseEndDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("(Optional)\nEnter Course End Date (YYYY-MM-DD): "), out inCourseEndDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Enter a date (yyyy-MM-dd): ");
            }

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

            ConsolePrintHelper.Pause();
        }
        public void AddTeacher()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new teacher");

            string? inTeacherFirstName = ConsolePrintHelper.AdminAskChoice("Enter First Name: ");
            string? inTeacherLastName = ConsolePrintHelper.AdminAskChoice("Enter Last Name: ");
            string? inTeacherEmail = ConsolePrintHelper.AdminAskChoice("Enter Email: ");

            if (string.IsNullOrWhiteSpace(inTeacherFirstName) || string.IsNullOrWhiteSpace(inTeacherLastName)
                || string.IsNullOrWhiteSpace(inTeacherEmail))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

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

            ConsolePrintHelper.Pause();
        }
        public void AddRoom()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new room");

            string? inRoomName = ConsolePrintHelper.AdminAskChoice("Enter Room Name: ");
            if (string.IsNullOrWhiteSpace(inRoomName))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            int inRoomCapacity = 0;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Room Capacity: "), out inRoomCapacity))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.Write("Enter a number: ");
            }

            var room = new Room
            {
                RoomName = inRoomName,
                Capacity = inRoomCapacity
            };

            if (db is not null)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                Console.WriteLine($"Room added: '{inRoomName}' | Capacity: {inRoomCapacity}");
            }

            ConsolePrintHelper.Pause();
        }
        public void AddEnrollment()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new enrollment");

            DateOnly inEnrollmentDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Enrollment Start Date (YYYY-MM-DD): "), out inEnrollmentDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Enter a date (yyyy-MM-dd): ");
            }

            int studentID;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Student ID: "), out studentID))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.Write("Enter student ID: ");
            }
            
            int courseID;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Course ID: "), out courseID))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.Write("Enter course ID: ");
            }
            
            // check if student and course exist 
            var findStudent = db.Students.FirstOrDefault(s  => s.StudentID == studentID);
            var findCourse = db.Courses.FirstOrDefault(c  => c.CourseID == courseID);
            
            // check if student is already enrolled in the course 
            var existingEnrollment = db.Enrollments.FirstOrDefault(e => e.FkCourseID == courseID && e.FkStudentID == studentID);
            if (existingEnrollment is not null)
            {
                Console.WriteLine("This student is already enrolled in this course.");
                return;
            }

            // if student and course are exist create enrollment
            if (findStudent is not null && findCourse is not null)
            {
                var enrollment = new Enrollment
                {
                    FkStudentID = studentID,
                    EnrollmentDate = inEnrollmentDate,
                    FkCourseID = courseID
                };

                if (db is not null)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    Console.WriteLine($"Enrollment added: {inEnrollmentDate}");
                }
            }
            else
            {
                Console.WriteLine($"Student or course not found");
            }
            ConsolePrintHelper.Pause();
        }
        public void FindStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Find student");

            string? inStudentName = ConsolePrintHelper.AdminAskChoice("Enter Name (First or Last):");

            if (string.IsNullOrWhiteSpace(inStudentName))
            {
                ConsolePrintHelper.PrintError("Email cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            if (db is not null)
            {
                var foundStudents = db.Students
                                    .Where(s => s.FirstName == inStudentName || s.LastName == inStudentName)
                                    .Select(s => $"({s.StudentID}) {s.FirstName} {s.LastName} | {s.Email}") 
                                    .ToList();

                ConsolePrintHelper.AdminMenu("-", foundStudents);
            }

            ConsolePrintHelper.Pause();
        }
        public void AddStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new student");

            string? inStudentFirstName = ConsolePrintHelper.AdminAskChoice("Enter First Name: ");
            string? inStudentLastName = ConsolePrintHelper.AdminAskChoice("Enter Last Name: ");
            string? inStudentEmail = ConsolePrintHelper.AdminAskChoice("Enter Email: ");

            if (string.IsNullOrWhiteSpace(inStudentFirstName) || string.IsNullOrWhiteSpace(inStudentLastName)
                || string.IsNullOrWhiteSpace(inStudentEmail))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            var student = new Student
            {
                FirstName = inStudentFirstName,
                LastName = inStudentLastName,
                Email = inStudentEmail,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
                StudentStatus = "Active"
            };

            if(db is not null)
            {
                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine($"Student added: '{inStudentFirstName} {inStudentLastName}' | {inStudentEmail} | {DateOnly.FromDateTime(DateTime.Today)} | {DateOnly.FromDateTime(DateTime.Today.AddYears(2))} | 'Active'"); 
            }

            ConsolePrintHelper.Pause();
        }
        public void RemoveStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Remove student");

            string? studentEmail = ConsolePrintHelper.AdminAskChoice("Enter Email:");

            if (string.IsNullOrWhiteSpace(studentEmail))
            {
                ConsolePrintHelper.PrintError("Email cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            if (db is not null)
            {
                var student = db.Students.FirstOrDefault(s => s.Email == studentEmail);
                Console.WriteLine($"Student removed: '{student.FirstName} {student.LastName}' | {student.Email}");
                db.Students.Remove(student);
                db.SaveChanges();
            }
            ConsolePrintHelper.Pause();
        }
        public void EditStudentMenu()
        {
        }

        public void ShowActiveCoursesWithStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Courses list");

            if (db is not null)
            {
                var courseData = db.Courses
                    .Select(c => $"({c.CourseID}) '{c.CourseName}' | {c.ActiveFrom}-{c.ActiveTo} | {c.CourseStatus}")
                    .ToList();

                ConsolePrintHelper.AdminMenu("-", courseData);
            }
            ConsolePrintHelper.Pause();
        }
        public void ShowAllStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Students list");

            if (db is not null)
            {
                var studentsName = db.Students
                    .Select(s => $"({s.StudentID}) {s.FirstName} {s.LastName} | {s.Email}")
                    .ToList();

                ConsolePrintHelper.AdminMenu("-", studentsName);
            }
            ConsolePrintHelper.Pause();
        }
        public void ShowStudentsPerTerm()
        {
        }
    }
}
