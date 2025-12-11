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
                    "ADD",
                    "VIEW",
                    "EDIT",
                    "REMOVE"
                    
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": AddMenu(); break;
                    case "2": ViewMenu(); break;
                    case "3": EditMenu(); break;
                    case "4": RemoveMenu(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        public void AddMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("ADD MENU", new List<string>
                {
                    "Add New Course",
                    "Add New Teacher",
                    "Add New Room",
                    "Add New Enrollment",
                    "Add New Student"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": AddCourse(); break;
                    case "2": AddTeacher(); break;
                    case "3": AddRoom(); break;
                    case "4": AddEnrollment(); break;
                    case "5": AddStudent(); break;
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
                    "Edit Student"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": EditStudentMenu(); break;
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
                    "Remove Student"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": RemoveStudent(); break;
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

            MainMenuServices.CreateCourse(db, inCourseName, inCourseStatus, inCourseStartDate, inCourseEndDate);

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

            MainMenuServices.CreateTeacher(db, inTeacherFirstName, inTeacherLastName, inTeacherEmail);

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

            // TODO: Find teacher in list and assign to room.
            string? inRoomTeacher = ConsolePrintHelper.AdminAskChoice("Assign Teacher To Room: ");
            if (string.IsNullOrWhiteSpace(inRoomTeacher))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return;
            }

            MainMenuServices.CreateRoom(db, inRoomName, inRoomCapacity, inRoomTeacher);

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

            MainMenuServices.CreateEnrollment(db, inEnrollmentDate);

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

            MainMenuServices.CreateStudent(db, inStudentFirstName, inStudentLastName, inStudentEmail, DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddYears(2)), "Active");

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
        public void EditStudentMenu()
        {
        }

        public void ShowActiveCoursesWithStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Courses list");

            var courseList = MainMenuServices.GetCoursesAsList(db);

            if(courseList != null)
                ConsolePrintHelper.AdminMenu("-", MainMenuServices.GetCoursesAsList(db));
            else
                Console.WriteLine("Courses could not be found!");

            ConsolePrintHelper.Pause();
        }
        public void ShowAllStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN MENU");
            ConsolePrintHelper.AdminSubTitle("Students list");

            ConsolePrintHelper.AdminMenu("-", MainMenuServices.GetStudentsAsList(db));

            ConsolePrintHelper.Pause();
        }
        public void ShowStudentsPerTerm()
        {
        }
    }
}
