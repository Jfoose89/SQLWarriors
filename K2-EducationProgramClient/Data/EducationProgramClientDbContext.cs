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

        // NOT USING JSON
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EducationProgramDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        // USING JSON
        //public EducationProgramClientDbContext(DbContextOptions<EducationProgramClientDbContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        
    }     
}
