using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.ViewModels
{
    public class FailingCourseReport
    {
        public int StudentID { get; set; }
        public string StudentFullName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string FinalGrade { get; set; } // Should be 'F'
    }
}
