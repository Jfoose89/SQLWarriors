using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public DateOnly ActiveFrom { get; set; }
        public DateOnly ActiveTo { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
