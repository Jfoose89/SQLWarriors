using K2_EducationProgramClient.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Data
{
    public class SeedData
    {
        public void AddSeedData(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                // Course: Requires none
                MainMenuServices.CreateCourse(db, "Course1", "Active", DateOnly.Parse("0001-01-01"), DateOnly.Parse("0001-01-01"));
                MainMenuServices.CreateCourse(db, "Course2", "Inactive", DateOnly.Parse("0002-02-02"), DateOnly.Parse("0002-02-02"));
                MainMenuServices.CreateCourse(db, "Course3", "Active", DateOnly.Parse("0003-03-03"), DateOnly.Parse("0003-03-03"));

                // Teacher: Requires none
                MainMenuServices.CreateTeacher(db, "TAlice", "TAlicesson", "talice@email.com");
                MainMenuServices.CreateTeacher(db, "TBob", "TBobsson", "tbob@email.com");
                MainMenuServices.CreateTeacher(db, "TCharlie", "TCharliesson", "tcharlie@email.com");

                // Student: Requires none
                MainMenuServices.CreateStudent(db, "SAlice", "SAlicesson", "salice@email.com", DateOnly.Parse("0001-01-01"), DateOnly.Parse("0001-01-01"), "Active");
                MainMenuServices.CreateStudent(db, "SBob", "SBobsson", "sbob@email.com", DateOnly.Parse("0002-02-02"), DateOnly.Parse("0002-02-02"), "Active");
                MainMenuServices.CreateStudent(db, "SCharlie", "SCharliesson", "scharlie@email.com", DateOnly.Parse("0003-03-03"), DateOnly.Parse("0003-03-03"), "Active");

                // Room: Requires teachers
                MainMenuServices.CreateRoom(db, "Room1", 1, 1);
                MainMenuServices.CreateRoom(db, "Room2", 2, 2);
                MainMenuServices.CreateRoom(db, "Room3", 3, 3);

                // Enrollment: Requires student, course
                MainMenuServices.CreateEnrollment(db, 1, 1, DateOnly.Parse("0001-01-01"));
                MainMenuServices.CreateEnrollment(db, 2, 2, DateOnly.Parse("0002-02-02"));
                MainMenuServices.CreateEnrollment(db, 3, 3, DateOnly.Parse("0003-03-03"));

                // Grade: Requires enrollment, teacher
                MainMenuServices.CreateGrade(db, 1, 1, DateOnly.Parse("0001-01-01"), "F");
                MainMenuServices.CreateGrade(db, 2, 2, DateOnly.Parse("0002-02-02"), "C");
                MainMenuServices.CreateGrade(db, 3, 3, DateOnly.Parse("0003-03-03"), "A");

                // TeacherCourse: Requires teacher, course
                MainMenuServices.CreateTeacherCourse(db, 1, 1);
                MainMenuServices.CreateTeacherCourse(db, 2, 2);
                MainMenuServices.CreateTeacherCourse(db, 3, 3);

                // Schedule: Requires teacher course, room
                MainMenuServices.CreateSchedule(db, DateOnly.Parse("0001-01-01"), 1, 1, DateTime.Parse("0001-01-01 01:01"), DateTime.Parse("0001-01-01 01:01"));
                MainMenuServices.CreateSchedule(db, DateOnly.Parse("0002-02-02"), 2, 2, DateTime.Parse("0002-02-02 02:02"), DateTime.Parse("0002-02-02 02:02"));
                MainMenuServices.CreateSchedule(db, DateOnly.Parse("0003-03-03"), 3, 3, DateTime.Parse("0003-03-03 03:03"), DateTime.Parse("0003-03-03 03:03"));
            }
        }

        public void ClearDatabase(EducationProgramClientDbContext? db)
        {
            if (db != null)
            {
                MainMenuServices.ClearAndReseedTable(db, "Schedules");
                MainMenuServices.ClearAndReseedTable(db, "TeacherCourses");
                MainMenuServices.ClearAndReseedTable(db, "Grades");
                MainMenuServices.ClearAndReseedTable(db, "Enrollments");
                MainMenuServices.ClearAndReseedTable(db, "Rooms");
                MainMenuServices.ClearAndReseedTable(db, "Students");
                MainMenuServices.ClearAndReseedTable(db, "Teachers");
                MainMenuServices.ClearAndReseedTable(db, "Courses");
            }
        }
    }
}
