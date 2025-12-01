using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class TeacherCourse
    {
        [Key]
        public int TeacherCourseID { get; set; }
        [ForeignKey("Teacher")]
        public int FkTeacherID { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey("Course")]
        public int FkCourseID { get; set; }
        public Course Course { get; set; }
        
        [ForeignKey("Schedule")]
        public int? FkScheduleID { get; set; }
        public Schedule? Schedule { get; set; }
    }
}
