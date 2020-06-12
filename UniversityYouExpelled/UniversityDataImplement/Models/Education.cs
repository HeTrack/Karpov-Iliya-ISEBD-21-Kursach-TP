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
        public int Id { get; set; }       
        public int ClientId { get; set; }       
        public DateTime EdCreate { get; set; }
        public int YearEd { get; set; }
        public string StatusEducation { get; set; }
        [Required]
        public decimal EdCost { get; set; }
        public EducationStatus Status { get; set; }
        [ForeignKey("EducationId")]
        public virtual List<EducationCourse> EducationCourses { get; set; }
        [Required]
        [ForeignKey("EducationId")]
        public List<Pay> Pays { get; set; }
        public Client Client { get; set; }
    }
}
