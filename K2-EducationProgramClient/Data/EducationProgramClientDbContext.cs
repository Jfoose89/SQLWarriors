using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using K2_EducationProgramClient.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using K2_EducationProgramClient.ViewModels;


namespace K2_EducationProgramClient.Data
{
    public class EducationProgramClientDbContext : DbContext
    {

        // NOT USING JSON
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EducationProgramDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

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

        //View Models
        public DbSet<StudentSummary> StudentSummaries { get; set; }
        public DbSet<CoursePerformance> CoursePerformances { get; set; }
        public DbSet<RoomUtilization> RoomUtilizations { get; set; }
        public DbSet<FailingCourseReport> FailingCourseReports { get; set; }

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

            // Schedule ↔ Room
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.FkRoomID);


            // Mapping View Models to Database Views

            // StudentSummary View
            modelBuilder.Entity<StudentSummary>().
                ToView("StudentSummaryView")
                .HasNoKey();
            // CoursePerformance View
            modelBuilder.Entity<CoursePerformance>()
                .ToView("CoursePerformanceView")
                .HasNoKey();
            // RoomUtilization View
            modelBuilder.Entity<RoomUtilization>()
                .ToView("RoomUtilizationView")
                .HasNoKey();
            // FailingCourseReport View
            modelBuilder.Entity<FailingCourseReport>()
                .ToView("FailingCourseReportView")
                .HasNoKey();

            // Disable EF Core’s OUTPUT clause for tables.
            // (EF Core + SQL Server limitation, SQL Server blocks operation when EF Core tries to use the OUTPUT clause to get identity values)
            modelBuilder.Entity<Enrollment>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Schedule>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Grade>()
                .ToTable(tb => tb.UseSqlOutputClause(false));
        }
    }
}     
