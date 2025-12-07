using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public int? FkTeacherID { get; set; }

        [ForeignKey("FkTeacherID")]        
        public Teacher? Teacher { get; set; }

        public required string RoomName { get; set; }
        public int Capacity { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}
