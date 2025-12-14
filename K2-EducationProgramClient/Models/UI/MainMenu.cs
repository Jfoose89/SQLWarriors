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
            ConsolePrintHelper.Banner();
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

        private void CreateMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("CREATE MENU", new List<string>
                {
                    "Create Course",
                    "Create Enrollment",
                    "Create Grade",
                    "Create Room",
                    "Create Schedule",
                    "Create Student",
                    "Create Teacher",
                    "Create Teacher Course"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": CreateCourse(); break;
                    case "2": CreateEnrollment(); break;
                    case "3": CreateGrade(); break;
                    case "4": CreateRoom();  break;
                    case "5": CreateSchedule();  break;
                    case "6": CreateStudent();  break;
                    case "7": CreateTeacher(); break;
                    case "8": CreateTeacherCourse(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        private void ViewMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("VIEW MENU", new List<string>
                {
                    "Select Table to View",
                    "Find Student",
                    "Show Students: Course, Grade, Teacher",
                    "Show All Active Courses and Enrolled Students",
                    "Show Student Report Per Term"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": ShowAllSelect();  break;
                    case "2": FindStudent(); break;
                    case "3": ShowStudentCourseGradeTeacher(); break;
                    case "4": ShowActiveCoursesWithStudents(); break;
                    case "5": ShowStudentReportByTerm(); break;
                    case "0": running = false; break;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        private void ShowAllSelect()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("SHOW ALL MENU", new List<string>
                {
                    "View Courses",
                    "View Enrollments",
                    "View Grades",
                    "View Rooms",
                    "View Schedules",
                    "View Students",
                    "View Teachers",
                    "View Teacher Courses"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1": 
                        ConsolePrintHelper.AdminList("COURSES", MainMenuServices.GetCoursesAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "2": 
                        ConsolePrintHelper.AdminList("ENROLLMENTS", MainMenuServices.GetEnrollmentsAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "3": 
                        ConsolePrintHelper.AdminList("GRADES", MainMenuServices.GetGradesAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "4": 
                        ConsolePrintHelper.AdminList("ROOMS", MainMenuServices.GetRoomsAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "5": 
                        ConsolePrintHelper.AdminList("SCHEDULES", MainMenuServices.GetSchedulesAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "6": 
                        ConsolePrintHelper.AdminList("STUDENTS", MainMenuServices.GetStudentsAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "7":
                        ConsolePrintHelper.AdminList("TEACHERS", MainMenuServices.GetTeachersAsList(db));
                        ConsolePrintHelper.Pause();
                        break;

                    case "8": 
                        ConsolePrintHelper.AdminList("TEACHER COURSES", MainMenuServices.GetTeacherCoursesAsList(db)); 
                        ConsolePrintHelper.Pause(); 
                        break;

                    case "0": 
                        running = false; 
                        break;

                    default: 
                        ConsolePrintHelper.FaultyMenuChoice(); 
                        break;
                }
            }
        }

        private void EditMenu()
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

        private void RemoveMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("REMOVE MENU", new List<string>
                {
                    "Remove Student",
                    "Select Table To Clear and Reseed"
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

        private void ClearAndResetTableMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("ADMIN");
                ConsolePrintHelper.AdminMenu("CLEAR & RESET MENU", new List<string>
                {
                    "Clear Courses",
                    "Clear Enrollments",
                    "Clear Grades",
                    "Clear Rooms",
                    "Clear Schedules",
                    "Clear Students",
                    "Clear Teachers",
                    "Clear Tearcher Courses"
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");
                if (db != null)
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

        private void CreateCourse()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new course");

            string? inCourseName = ConsolePrintHelper.AdminAskChoice("Enter Course Name: ");
            if (ConsolePrintHelper.NullInputWarning(inCourseName)) return;
            string? inCourseStatus = ConsolePrintHelper.AdminAskChoice("Enter Course Status: ");
            if (ConsolePrintHelper.NullInputWarning(inCourseStatus)) return;
            

            DateOnly inCourseStartDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Course Start Date (YYYY-MM-DD): "), out inCourseStartDate))
            {
                Console.WriteLine(" Invalid input. Please enter a valid date.");
                ConsolePrintHelper.Pause();
                return;
            }
            
            DateOnly inCourseEndDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("(Optional)\nEnter Course End Date (YYYY-MM-DD): "), out inCourseEndDate))
            {
                Console.WriteLine(" Invalid input. Please enter a valid date.");
                ConsolePrintHelper.Pause();
                return;
            }

            MainMenuServices.CreateCourse(db, inCourseName, inCourseStatus, inCourseStartDate, inCourseEndDate);

            ConsolePrintHelper.Pause();
        }
        private void CreateTeacher()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new teacher");

            string? inTeacherFirstName = ConsolePrintHelper.AdminAskChoice("Enter First Name: ");
            if (ConsolePrintHelper.NullInputWarning(inTeacherFirstName)) return;
            string? inTeacherLastName = ConsolePrintHelper.AdminAskChoice("Enter Last Name: ");
            if (ConsolePrintHelper.NullInputWarning(inTeacherLastName)) return;
            string? inTeacherEmail = ConsolePrintHelper.AdminAskChoice("Enter Email: ");
            if (ConsolePrintHelper.NullInputWarning(inTeacherEmail)) return;

            MainMenuServices.CreateTeacher(db, inTeacherFirstName, inTeacherLastName, inTeacherEmail);

            ConsolePrintHelper.Pause();
        }

        private void CreateTeacherCourse()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new course");
            ConsolePrintHelper.AdminList("TEACHERS", MainMenuServices.GetTeachersAsList(db));
            string? inTeacherID = ConsolePrintHelper.AdminAskChoice("Enter Teacher ID:");
            bool exists = db.Teachers.Any(t => t.TeacherID == int.Parse(inTeacherID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Teacher with ID ({inTeacherID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }
            if (!int.TryParse(inTeacherID, out int validTeacherID))
                Console.WriteLine(" Invalid input. Please enter a valid integer.");

            ConsolePrintHelper.AdminList("COURSES", MainMenuServices.GetCoursesAsList(db));
            string? inCourseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID:");
            exists = db.Courses.Any(c => c.CourseID == int.Parse(inCourseID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Course with ID ({inCourseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }
            if (!int.TryParse(inCourseID, out int validCourseID))
                Console.WriteLine(" Invalid input. Please enter a valid integer.");

            MainMenuServices.CreateTeacherCourse(db, validTeacherID, validCourseID);

            ConsolePrintHelper.Pause();
        }
        private void CreateRoom()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new room");

            string? inRoomName = ConsolePrintHelper.AdminAskChoice("Enter Room Name: ");
            if (ConsolePrintHelper.NullInputWarning(inRoomName)) return;

            int inRoomCapacity = 0;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Room Capacity: "), out inRoomCapacity))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter a number: ");
            }

            ConsolePrintHelper.AdminList("TEACHERS", MainMenuServices.GetTeachersAsList(db));
            string? inRoomTeacherID = ConsolePrintHelper.AdminAskChoice("Assign Teacher To Room using [ID]: ");
            if (ConsolePrintHelper.NullInputWarning(inRoomTeacherID)) return;
            if(!int.TryParse(inRoomTeacherID, out int validTeacherID))
                Console.WriteLine(" Invalid input. Please enter a valid integer.");

            MainMenuServices.CreateRoom(db, inRoomName, inRoomCapacity, validTeacherID);

            ConsolePrintHelper.Pause();
        }
        private void CreateEnrollment()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new enrollment");

            ConsolePrintHelper.AdminList("COURSES", MainMenuServices.GetCoursesAsList(db));
            string? inCourseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID: ");
            if (ConsolePrintHelper.NullInputWarning(inCourseID)) return;
            bool exists = db.Courses.Any(c => c.CourseID == int.Parse(inCourseID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Course with ID ({inCourseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("STUDENTS", MainMenuServices.GetStudentsAsList(db));
            string? inStudentID = ConsolePrintHelper.AdminAskChoice("Enter Student ID:");
            exists = db.Students.Any(s => s.StudentID == int.Parse(inStudentID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Student with ID ({inStudentID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            DateOnly inEnrollmentDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Enrollment Start Date (YYYY-MM-DD): "), out inEnrollmentDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Enter a date (yyyy-MM-dd): ");
            }

            MainMenuServices.CreateEnrollment(db, int.Parse(inCourseID), int.Parse(inStudentID), inEnrollmentDate);
            
            ConsolePrintHelper.Pause();
        }

        private void CreateGrade()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new grade");

            ConsolePrintHelper.AdminList("ENROLLMENTS", MainMenuServices.GetEnrollmentsAsList(db));
            string? inEnrollmentID = ConsolePrintHelper.AdminAskChoice("Enter Enrollment ID: ");
            if (ConsolePrintHelper.NullInputWarning(inEnrollmentID)) return;
            bool exists = db.Enrollments.Any(e => e.EnrollmentID == int.Parse(inEnrollmentID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Enrollment with ID ({inEnrollmentID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inGradeValue = ConsolePrintHelper.AdminAskChoice("Enter Grade Value (A,B,C,D,F): ");
            if (ConsolePrintHelper.NullInputWarning(inGradeValue)) return;

            ConsolePrintHelper.AdminList("TEACHERS", MainMenuServices.GetTeachersAsList(db));
            string? inTeacherID = ConsolePrintHelper.AdminAskChoice("Enter Teacher ID:");
            exists = db.Teachers.Any(t => t.TeacherID == int.Parse(inTeacherID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($"Teacher with ID ({inTeacherID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            DateOnly inGradeDate;
            while (!DateOnly.TryParse(ConsolePrintHelper.AdminAskChoice("Enter Grading Date (YYYY-MM-DD): "), out inGradeDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Enter a date (yyyy-MM-dd): ");
            }

            MainMenuServices.CreateGrade(db, int.Parse(inEnrollmentID), int.Parse(inTeacherID), inGradeDate, inGradeValue);

            ConsolePrintHelper.Pause();
        }
        private void FindStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Find student");

            string? inStudentName = ConsolePrintHelper.AdminAskChoice("Enter Name (First or Last):");

            if (inStudentName != null)
                MainMenuServices.FindStudentByNameAndPrintInfo(db, inStudentName);
            else
                Console.WriteLine("Student name cannot be empty!");

            ConsolePrintHelper.Pause();
        }
        private void CreateStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new student");

            string? inStudentFirstName = ConsolePrintHelper.AdminAskChoice("Enter First Name: ");
            if (ConsolePrintHelper.NullInputWarning(inStudentFirstName)) return;
            string? inStudentLastName = ConsolePrintHelper.AdminAskChoice("Enter Last Name: ");
            if (ConsolePrintHelper.NullInputWarning(inStudentLastName)) return;
            string? inStudentEmail = ConsolePrintHelper.AdminAskChoice("Enter Email: ");
            if (ConsolePrintHelper.NullInputWarning(inStudentEmail)) return;

            MainMenuServices.CreateStudent(db, inStudentFirstName, inStudentLastName, inStudentEmail, DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddYears(2)), "Active");

            ConsolePrintHelper.Pause();
        }

        private void CreateSchedule()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Create new schedule");

            string? inDate = ConsolePrintHelper.AdminAskChoice("Enter Schedule Date (YYYY-MM-DD): ");
            if (ConsolePrintHelper.NullInputWarning(inDate)) return;
            if (!DateOnly.TryParse(inDate, out DateOnly validDate))
            {
                Console.WriteLine(" Invalid date format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("COURSES", MainMenuServices.GetCoursesAsList(db));
            string? inCourseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID: ");
            if (ConsolePrintHelper.NullInputWarning(inCourseID)) return;
            bool exists = db.Courses.Any(c => c.CourseID == int.Parse(inCourseID));
            if(!exists)
            {
                ConsolePrintHelper.PrintError($" Course with ID ({inCourseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("ROOMS", MainMenuServices.GetRoomsAsList(db));
            string? inRoomID = ConsolePrintHelper.AdminAskChoice("Enter Room ID: ");
            if (ConsolePrintHelper.NullInputWarning(inRoomID)) return;
            exists = db.Rooms.Any(r => r.RoomID == int.Parse(inRoomID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Room with ID ({inRoomID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("TEACHERS", MainMenuServices.GetTeachersAsList(db));
            string? inTeacherID = ConsolePrintHelper.AdminAskChoice("Enter Teacher ID: ");
            if(ConsolePrintHelper.NullInputWarning(inTeacherID)) return;
            exists = db.Teachers.Any(t => t.TeacherID == int.Parse(inTeacherID));
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Teacher with ID ({inTeacherID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inStartTime = ConsolePrintHelper.AdminAskChoice("Enter Start Time (HH:MI:SS): ");
            if(ConsolePrintHelper.NullInputWarning(inStartTime)) return;
            if (!DateTime.TryParse($"{inDate} {inStartTime}", out DateTime validStarTime))
            {
                Console.WriteLine(" Invalid time format. Please try again.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inEndTime = ConsolePrintHelper.AdminAskChoice("Enter End Time (HH:MI:SS): ");
            if(ConsolePrintHelper.NullInputWarning(inEndTime)) return;
            if (!DateTime.TryParse($"{inDate} {inEndTime}", out DateTime validEndTime))
            {
                Console.WriteLine(" Invalid time format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }

            MainMenuServices.CreateSchedule(db, DateOnly.Parse(inDate), int.Parse(inCourseID), int.Parse(inRoomID), validStarTime, validEndTime);

            ConsolePrintHelper.Pause();
        }

        private void RemoveStudent()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Remove student");

            ConsolePrintHelper.AdminList("STUDENTS", MainMenuServices.GetStudentsAsList(db));
            string? studentEmail = ConsolePrintHelper.AdminAskChoice("Enter Student Email to Remove:");

            MainMenuServices.DeleteStudentByEmail(db, studentEmail);

            ConsolePrintHelper.Pause();
        }
        private void RegisterStudentToCourse()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Register Student To Course");

            ConsolePrintHelper.AdminList("STUDENTS", MainMenuServices.GetStudentsAsList(db));
            string? studentID = ConsolePrintHelper.AdminAskChoice("Enter Student ID:");
            int.TryParse(studentID, out int validStudentID);
            bool exists = db.Students.Any(s => s.StudentID == validStudentID);
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Student with ID ({studentID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("COURSES", MainMenuServices.GetCoursesAsList(db));
            string? courseID = ConsolePrintHelper.AdminAskChoice("Enter Course ID:");
            int.TryParse(courseID, out int validCourseID);
            exists = db.Courses.Any(c => c.CourseID == validCourseID);
            if (!exists)
            {
                ConsolePrintHelper.PrintError($" Course with ID ({courseID}) not found.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inDate = ConsolePrintHelper.AdminAskChoice("Enter Enrollment Date (YYYY-MM-DD): ");
            if(ConsolePrintHelper.NullInputWarning(inDate)) return;

            try
            {
                // New enrollment
                MainMenuServices.RegisterStudentToCourseByID(db, int.Parse(studentID), int.Parse(courseID), DateOnly.Parse(inDate));
            }
            catch (DuplicateNameException ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Unexpected error: {ex.Message}");
            }

            ConsolePrintHelper.Pause();
        }
        private void ShowStudentCourseGradeTeacher()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Student - Course, Grade, Teacher");

            var studentsList = MainMenuServices.GetStudentCourseGradeTeacherList(db);

            if (studentsList != null)
            {
                ConsolePrintHelper.AdminList("STUDENTS", studentsList);
            }
            else
            {
                Console.WriteLine(" Courses could not be found!");
            }
                

            ConsolePrintHelper.Pause();
        }
        private void ShowActiveCoursesWithStudents()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Active Courses with Students");

            var courseList = MainMenuServices.GetActiveCoursesWithRegisteredStudentsList(db);

            if(courseList != null)
                ConsolePrintHelper.AdminList("COURSES", courseList);
            else
                Console.WriteLine(" Courses could not be found!");

            ConsolePrintHelper.Pause();
        }

        public void ShowStudentReportByTerm()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("ADMIN");
            ConsolePrintHelper.AdminSubTitle("Students by term");

            string? inStartDate = ConsolePrintHelper.AdminAskChoice("Start Date (YYYY-MM-DD):");
            if(ConsolePrintHelper.NullInputWarning(inStartDate)) return;
            if (!DateOnly.TryParse(inStartDate, out DateOnly validStartDate))
            {
                Console.WriteLine(" Invalid date format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }

            string? inEndDate = ConsolePrintHelper.AdminAskChoice("End Date (YYYY-MM-DD):");
            if (ConsolePrintHelper.NullInputWarning(inEndDate)) return;
            if (!DateOnly.TryParse(inEndDate, out DateOnly validEndDate))
            {
                Console.WriteLine(" Invalid date format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }

            ConsolePrintHelper.AdminList("REPORT", MainMenuServices.GetStudentApprovalReportByTermList(db, validStartDate, validEndDate));

            ConsolePrintHelper.Pause();
        }
    }
}
