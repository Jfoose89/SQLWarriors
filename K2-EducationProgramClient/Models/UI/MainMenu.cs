using K2_EducationProgramClient.Data;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
                    "CREATE",
                    "VIEW",
                    "EDIT",
                    "REMOVE"
                    
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": CreateMenu(); break;
                    case "2": ViewMenu(); break;
                    case "3": EditMenu(); break;
                    case "4": RemoveMenu(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void CreateMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("ADD MENU", new List<string>
                {
                    "New Course",
                    "New Teacher",
                    "New Room",
                    "New Enrollment",
                    "New Student",
                    "New Schedule",
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": CreateCourse(); break;
                    case "2": CreateTeacher(); break;
                    case "3": CreateRoom(); break;
                    case "4": CreateEnrollment(); break;
                    case "5": CreateStudent(); break;
                    case "6": CreateSchedule(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void ViewMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("VIEW MENU", new List<string>
                {
                    "Show All Students",
                    "Find Student",
                    "Show All Active Courses and Enrolled Students",
                    "Show Student Report Per Term."
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": ShowAllStudents();  break;
                    case "2": FindStudent(); break;
                    case "3": ShowActiveCoursesWithStudents(); break;
                    case "4": ShowStudentsPerTerm(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void EditMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("EDIT MENU", new List<string>
                {
                    "Register Student To Course"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": RegisterStudentToCourse(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void RemoveMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("REMOVE MENU", new List<string>
                {
                    "Remove Student",
                    "Clear and reset data table"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": RemoveStudent(); break;
                    case "2": ClearAndResetTableMenu(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void ClearAndResetTableMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("CLEAR & RESET MENU", new List<string>
                {
                    "Courses",
                    "Enrollments",
                    "Grades",
                    "Rooms",
                    "Schedules",
                    "Students",
                    "Teachers",
                    "Tearcher Courses"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");
                if (db is not null)
                {
                    switch (choice)
                    {
                        case "1": MainMenuServices.ClearAndReseedTable(db, "Courses"); ConsolePrintHelper.Pause(); break;
                        case "2": MainMenuServices.ClearAndReseedTable(db, "Enrollments"); ConsolePrintHelper.Pause(); break;
                        case "3": MainMenuServices.ClearAndReseedTable(db, "Grades"); ConsolePrintHelper.Pause(); break;
                        case "4": MainMenuServices.ClearAndReseedTable(db, "Rooms"); ConsolePrintHelper.Pause(); break;
                        case "5": MainMenuServices.ClearAndReseedTable(db, "Schedules"); ConsolePrintHelper.Pause(); break;
                        case "6": MainMenuServices.ClearAndReseedTable(db, "Students"); ConsolePrintHelper.Pause(); break;
                        case "7": MainMenuServices.ClearAndReseedTable(db, "Teachers"); ConsolePrintHelper.Pause(); break;
                        case "8": MainMenuServices.ClearAndReseedTable(db, "TearcherCourses"); ConsolePrintHelper.Pause(); break;
                        case "0": running = false; break;
                        default: ConsolePrintHelper.FaultyMenuChoice(); break;
                    }
                }
            }
        }

        public void CreateCourse()
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

            MainMenuServices.CreateCourse(db, inCourseName, inCourseStatus, inCourseStartDate, inCourseEndDate);

            ConsolePrintHelper.Pause();
        }
        public void CreateTeacher()
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

            MainMenuServices.CreateTeacher(db, inTeacherFirstName, inTeacherLastName, inTeacherEmail);

            ConsolePrintHelper.Pause();
        }
        public void CreateRoom()
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

            ConsolePrintHelper.AdminList("Teachers", MainMenuServices.GetTeachersAsList(db));
            string? inRoomTeacherID = ConsolePrintHelper.AdminAskChoice("Assign Teacher To Room: ");
            if (string.IsNullOrWhiteSpace(inRoomTeacherID))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            MainMenuServices.CreateRoom(db, inRoomName, inRoomCapacity, int.Parse(inRoomTeacherID));

            ConsolePrintHelper.Pause();
        }
        public void CreateEnrollment()
        {
            //Console.Clear();
            //ConsolePrintHelper.AdminTitle("ADMIN MENU");
            //ConsolePrintHelper.AdminSubTitle("Create new enrollment");

            //DateOnly inEnrollmentDate;
            //while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Enrollment Start Date (YYYY-MM-DD): "), out inEnrollmentDate))
            //{
            //    Console.WriteLine("Invalid input. Please enter a valid date.");
            //    Console.Write("Enter a date (yyyy-MM-dd): ");
            //}

            //MainMenuServices.CreateEnrollment(db, inEnrollmentDate);

            ConsolePrintHelper.Pause();
        }
        public void FindStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Find student");

            string? inStudentName = ConsolePrintHelper.AdminAskChoice("Enter Name (First or Last):");

            if (inStudentName != null)
                MainMenuServices.FindStudentByNameAndPrintInfo(db, inStudentName);
            else
                Console.WriteLine("Student name cannot be empty!");

            ConsolePrintHelper.Pause();
        }
        public void CreateStudent()
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

            MainMenuServices.CreateStudent(db, inStudentFirstName, inStudentLastName, inStudentEmail, DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddYears(2)), "Active");

            ConsolePrintHelper.Pause();
        }

        public void CreateSchedule()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Create new schedule");

            string? inDate = ConsolePrintHelper.AdminAskChoice("Enter Schedule Date (YYYY-MM-DD): ");
            ConsolePrintHelper.NullInputWarning(inDate);

            ConsolePrintHelper.AdminList("Courses", MainMenuServices.GetCoursesAsList(db));
            string? inCourseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID: ");
            ConsolePrintHelper.NullInputWarning(inCourseID);
            bool exists = db.Courses.Any(c => c.CourseID == int.Parse(inCourseID));
            if(!exists)
            {
                ConsolePrintHelper.PrintError($"Course with ID ({inCourseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("Rooms", MainMenuServices.GetRoomsAsList(db));
            string? inRoomID = ConsolePrintHelper.AdminAskChoice("Enter Room ID: ");
            ConsolePrintHelper.NullInputWarning(inRoomID);
            exists = db.Rooms.Any(r => r.RoomID == int.Parse(inRoomID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Room with ID ({inRoomID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("Teachers", MainMenuServices.GetTeachersAsList(db));
            string? inTeacherID = ConsolePrintHelper.AdminAskChoice("Enter Teacher ID: ");
            ConsolePrintHelper.NullInputWarning(inTeacherID);
            exists = db.Teachers.Any(t => t.TeacherID == int.Parse(inTeacherID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Teacher with ID ({inTeacherID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inStartTime = ConsolePrintHelper.AdminAskChoice("Enter Start Time (HH:MI:SS): ");
            ConsolePrintHelper.NullInputWarning(inStartTime);

            string? inEndTime = ConsolePrintHelper.AdminAskChoice("Enter End Time (HH:MI:SS): ");
            ConsolePrintHelper.NullInputWarning(inEndTime);

            MainMenuServices.CreateSchedule(db, DateOnly.Parse(inDate), int.Parse(inCourseID), int.Parse(inRoomID), DateTime.Parse($"{inDate} {inStartTime}"), DateTime.Parse($"{inDate} {inEndTime}"));

            ConsolePrintHelper.Pause();
        }

        public void RemoveStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Remove student");

            string? studentEmail = ConsolePrintHelper.AdminAskChoice("Enter Email:");

            MainMenuServices.DeleteStudentByEmail(db, studentEmail);

            ConsolePrintHelper.Pause();
        }
        public void RegisterStudentToCourse()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Register Student To Course");

            ConsolePrintHelper.AdminList("Students", MainMenuServices.GetStudentsAsList(db));
            string? studentID = ConsolePrintHelper.AdminAskChoice("Enter Student ID:");
            bool exists = db.Students.Any(s => s.StudentID == int.Parse(studentID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Student with ID ({studentID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("Courses", MainMenuServices.GetCoursesAsList(db));
            string? courseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID:");
            exists = db.Courses.Any(c => c.CourseID == int.Parse(courseID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Course with ID ({courseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inDate = ConsolePrintHelper.AdminAskChoice("Enter Enrollment Date (YYYY-MM-DD): ");
            ConsolePrintHelper.NullInputWarning(inDate);

            try
            {
                // New enrollment
                MainMenuServices.RegisterStudentToCourseByID(db, int.Parse(studentID), int.Parse(courseID), DateOnly.Parse(inDate));
            }
            catch (DuplicateNameException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            ConsolePrintHelper.Pause();
        }

        public void ShowActiveCoursesWithStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Courses list");

            var courseList = MainMenuServices.GetCoursesAsList(db);

            if(courseList != null)
                ConsolePrintHelper.AdminList("-", MainMenuServices.GetCoursesAsList(db));
            else
                Console.WriteLine("Courses could not be found!");

            ConsolePrintHelper.Pause();
        }
        public void ShowAllStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Students list");

            ConsolePrintHelper.AdminList("-", MainMenuServices.GetStudentsAsList(db));

            ConsolePrintHelper.Pause();
        }
        public void ShowStudentsPerTerm()
        {
        }
    }
}
