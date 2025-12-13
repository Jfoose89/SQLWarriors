using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.ViewModels
{
    public class CoursePerformance
    {
        public string CourseName { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalSchedules { get; set; }
        public decimal AverageCourseGrade { get; set; }
        public int FailingStudentsCount { get; set; }
    }
}
