using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        public DateOnly Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int FkTeacherCourseID { get; set; }
        public int FkRoomID { get; set; }


        [ForeignKey("FkTeacherCourseID")]       
        public required TeacherCourse TeacherCourse { get; set; }

        [ForeignKey("FkRoomID")]        
        public required Room Room { get; set; }
    }
}
