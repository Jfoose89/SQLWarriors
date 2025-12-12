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
        public int FkTeacherID { get; set; }
        public int FkCourseID { get; set; }

        [ForeignKey("FkTeacherID")]        
        public Teacher Teacher { get; set; }

        [ForeignKey("FkCourseID")]       
        public Course Course { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}
