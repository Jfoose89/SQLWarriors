using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.ViewModels
{
    public class StudentSummary
    {
        public int StudentID { get; set; }
        public string StudentFullName { get; set; }
        public string StudentStatus { get; set; }
        public int TotalCoursesEnrolled { get; set; }
        public decimal AverageGradeValue { get; set; }
    }
}
