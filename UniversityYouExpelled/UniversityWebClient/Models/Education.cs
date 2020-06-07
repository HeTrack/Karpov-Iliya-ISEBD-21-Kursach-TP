using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityBusinessLogic.Enums;

namespace UniversityWebClient.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime EdCreate { get; set; }
        public int YearEd { get; set; }
        public string StatusEducation { get; set; }
        public decimal EdCost { get; set; }
        public decimal PaidSum { get; set; }
        public decimal Remain { get; set; }
        public EducationStatus Status { get; set; }
        public List<Course> EducationCourses { get; set; }
    }
}
