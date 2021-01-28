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
        public int ID { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lecturer { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
