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

        public int FkEnrollmentID { get; set; }
        public int FkTeacherID { get; set; }

        [ForeignKey("FkEnrollmentID")]        
        public Enrollment Enrollment { get; set; }

        [ForeignKey("FkTeacherID")]        
        public Teacher Teacher { get; set; }

        public DateOnly GradeDate { get; set; }
        public string GradeValue { get; set; }
    }
}
