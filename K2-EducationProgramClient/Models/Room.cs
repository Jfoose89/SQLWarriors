using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
