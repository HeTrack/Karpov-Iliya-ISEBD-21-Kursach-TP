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
        public int Id { get; set; }
        [Required]
        public int? EducationId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Course Course { get; set; }
        public virtual Education Education { get; set; }
    }
}
