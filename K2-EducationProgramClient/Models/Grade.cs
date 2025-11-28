using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models
{
    public class Grade
    {
        [Key]
        public int GradeID { get; set; }
        public DateOnly GradeDate { get; set; }
        public string GradeValue { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        [ForeignKey("Enrollment")]
        public int FkEnrollmentID { get; set; }
        public Enrollment Enrollment { get; set; }

        [ForeignKey("Teacher")]
        public int FkTeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
