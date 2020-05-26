using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusinessLogic.Enums;

namespace UniversityDatabaseImplement.Models
{
    public class Education
    {
        public int? Id { get; set; }
        [Required]
        public int? ClientId { get; set; }
        [Required]
        public DateTime EdCreate { get; set; }
        [Required]
        public int YearEd { get; set; }
        [Required]
        public string StatusEducation { get; set; }
        [Required]
        public decimal EdCost { get; set; }
        [Required]
        public PayStatus Status { get; set; }
        public virtual List<EducationCourse> EducationCourse { get; set; }
        public virtual List<Pay> Pays { get; set; }
        public Client Client { get; set; }
    }
}
