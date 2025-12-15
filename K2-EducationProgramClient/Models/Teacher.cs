using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<TeacherCourse>? TeacherCourses { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
