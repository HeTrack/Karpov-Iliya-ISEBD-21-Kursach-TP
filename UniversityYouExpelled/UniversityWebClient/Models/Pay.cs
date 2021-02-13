using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityWebClient.Models
{
    public class Pay
    {
        [Required]
        public decimal PaySum { get; set; }
        public int EducationID { get; set; }
        public int CourseID { get; set; }
    }
}
