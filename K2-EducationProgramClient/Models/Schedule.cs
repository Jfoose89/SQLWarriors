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
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        [ForeignKey("Course")]
        public int FkCourseID { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Room")]
        public int FkRoomID { get; set; }
        public Room Room { get; set; }

        [ForeignKey("Teacher")]
        public int FkTeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
