using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDataBaseImplement.Models
{
   public class Course
    {
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string LecturerFIO { get; set; }
        [Required]
        public DateTime StartCourse { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
