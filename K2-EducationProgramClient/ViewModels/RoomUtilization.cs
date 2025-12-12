using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.ViewModels
{
    public class RoomUtilization
    {
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int TotalScheduledHours { get; set; }
        public int PeakEnrollment { get; set; } // Max students in any schedule for this room
        public decimal UtilizationPercentage { get; set; }
    }
}
