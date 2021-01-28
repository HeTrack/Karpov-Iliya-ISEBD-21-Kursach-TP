using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDataBaseImplement.Models
{
   public class EducationCourse
    {
        public int ID { get; set; }
        public int? EducationID { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
        public virtual Education Education { get; set; }
    }
}
