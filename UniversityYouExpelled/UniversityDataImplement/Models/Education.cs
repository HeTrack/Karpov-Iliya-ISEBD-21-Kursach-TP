using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusinessLogic.Enums;

namespace UniversityDataBaseImplement.Models
{
    public class Education
    {
        public int ID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int YearED { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public decimal CostED { get; set; }
        [Required]
        public PayStatus PayStatus { get; set; }
        [Required]
        [ForeignKey("EducationID")]
        public virtual List<EducationCourse> EducationCourses { get; set; }
        [Required]
        [ForeignKey("EducationID")]
        public List<Pay> Pays { get; set; }
        public Client Client { get; set; }
    }
}
