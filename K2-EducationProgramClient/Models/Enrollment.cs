using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int FkStudentID { get; set; }
        public int FkCourseID { get; set; }

        [ForeignKey("FkStudentID")]        
        public Student Student { get; set; }

        [ForeignKey("FkCourseID")]        
        public Course Course { get; set; }

        public DateOnly EnrollmentDate { get; set; }
        public ICollection<Grade>? Grades { get; set; }

    }
}
