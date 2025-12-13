using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using K2_EducationProgramClient.Models;


namespace K2_EducationProgramClient.Data
{
    public class EducationProgramClientDbContext : DbContext
    {
        //private readonly bool _skipOnConfiguring;

        //public EducationProgramClientDbContext(DbContextOptions<EducationProgramClientDbContext> options, bool skipOnConfiguring = false) : base(options)
        //{
        //    _skipOnConfiguring = skipOnConfiguring;
        //}

        //// NOT USING JSON
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (_skipOnConfiguring) return;

        //    if (!optionsBuilder.IsConfigured)
        //    {
        //            optionsBuilder.UseSqlServer("Server=localhost;Database=EducationProgramDB;Trusted_Connection=True;TrustServerCertificate=True");
        //    }
        //}

        // USING JSON
        public EducationProgramClientDbContext(DbContextOptions<EducationProgramClientDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enrollment: Student ↔ Course
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.FkStudentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.FkCourseID);

            // Teacher ↔ Course (TeacherCourse)
            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourses)
                .HasForeignKey(tc => tc.FkTeacherID);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(tc => tc.FkCourseID);

            // Schedule ↔ Room, Course, Teacher
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.FkRoomID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.FkCourseID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.FkTeacherID);

            // Seed Data
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentID = 1, FirstName = "Adchariya", LastName = "Changtam", Email = "adchariya.changtam@example.com"},
                new Student { StudentID = 2, FirstName = "Coday", LastName = "Awahmed", Email = "coday.awahmed@example.com" },
                new Student { StudentID = 3, FirstName = "Hande", LastName = "Bengu", Email = "hande.bengu@example.com" },
                new Student { StudentID = 4, FirstName = "Jordan", LastName = "Foose", Email = "jordan.foose@example.com" }
                );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseID = 1, CourseName = "Mathematics 1", CourseStatus = "Active", ActiveFrom = new DateOnly(2025, 1, 1) },
                new Course { CourseID = 2, CourseName = "Programming C#", CourseStatus = "Active", ActiveFrom = new DateOnly(2025, 1, 1) },
                new Course { CourseID = 3, CourseName = "Swedish Literature", CourseStatus = "Active", ActiveFrom = new DateOnly(2025, 1, 1) },
                new Course { CourseID = 4, CourseName = "World Geography", CourseStatus = "Active", ActiveFrom = new DateOnly(2025, 1, 1) }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room { RoomID = 1, RoomName = "A101", Capacity = 30 },
                new Room { RoomID = 2, RoomName = "B202", Capacity = 25 }
                );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherID = 1, FirstName = "Eva", LastName = "Eriksson", Email = "eva.eriksson@example.com"},
                new Teacher { TeacherID = 2, FirstName = "David", LastName = "Dahl", Email = "david.dahl@example.com"}
                );

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Schedule)
                .WithMany() 
                .HasForeignKey(tc => tc.FkScheduleID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TeacherCourse>().HasData(
                new TeacherCourse { TeacherCourseID = 1, FkTeacherID = 1, FkCourseID = 1, FkScheduleID = 1 },
                new TeacherCourse { TeacherCourseID = 2, FkTeacherID = 2, FkCourseID = 2, FkScheduleID = 2 }
                );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentID = 1, FkStudentID = 1, FkCourseID = 1 },
                new Enrollment { EnrollmentID = 2, FkStudentID = 2, FkCourseID = 2 },
                new Enrollment { EnrollmentID = 3, FkStudentID = 3, FkCourseID = 1 },
                new Enrollment { EnrollmentID = 4, FkStudentID = 4, FkCourseID = 2 }
                );

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Enrollment)
                .WithMany()
                .HasForeignKey(g => g.FkEnrollmentID);

            modelBuilder.Entity<Grade>().HasData(
                new Grade { GradeID = 1, FkEnrollmentID = 1, FkTeacherID = 1, GradeDate = new DateOnly(2025, 12, 15), GradeValue = "A" },
                new Grade { GradeID = 2, FkEnrollmentID = 2, FkTeacherID = 2, GradeDate = new DateOnly(2025, 12, 16), GradeValue = "B" },
                new Grade { GradeID = 3, FkEnrollmentID = 3, FkTeacherID = 2, GradeDate = new DateOnly(2025, 12, 17), GradeValue = "C" },
                new Grade { GradeID = 4, FkEnrollmentID = 4, FkTeacherID = 1, GradeDate = new DateOnly(2025, 12, 18), GradeValue = "B" }
                );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { ScheduleID = 1, FkRoomID = 1, FkCourseID = 1, FkTeacherID = 1, Date = new DateOnly(2025, 12, 15), StartTime = new DateTime(2025, 12, 15, 8, 0, 0), EndTime = new DateTime(2025, 12, 15, 10, 0, 0) },
                new Schedule { ScheduleID = 2, FkRoomID = 2, FkCourseID = 2, FkTeacherID = 2, Date = new DateOnly(2025, 12, 16), StartTime = new DateTime(2025, 12, 16, 13, 0, 0), EndTime = new DateTime(2025, 12, 16, 15, 0, 0) }
                );
        }
    }
}     
