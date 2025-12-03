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

        [ForeignKey("CourseID")]
        public int FkCourseID { get; set; }
        public required Course Course { get; set; }

        [ForeignKey("RoomID")]
        public int FkRoomID { get; set; }
        public required Room Room { get; set; }

        [ForeignKey("TeacherID")]
        public int FkTeacherID { get; set; }
        public required Teacher Teacher { get; set; }
    }
}
