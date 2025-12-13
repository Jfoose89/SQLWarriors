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

        [ForeignKey(nameof(Teacher))]
        public int FkTeacherID { get; set; }
        public int FkCourseID { get; set; }

        [ForeignKey("FkTeacherID")]        
        public Teacher Teacher { get; set; }

        [ForeignKey(nameof(Course))]
        public int FkCourseID { get; set; }
        public Course Course { get; set; }
        
        [ForeignKey(nameof(Schedule))]
        public int FkScheduleID { get; set; }
        public Schedule Schedule { get; set; }
    }
}
