using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BindingModels
{
   public class EducationBindingModel
    {
        public int Id { get; set; }
        public DateTime EducationCreate { get; set; }
        public int YearEd { get; set; }
        public string StatusEducation { get; set; }
        public decimal EdCost { get; set; }        
        public PayStatus Status { get; set; }
        public Dictionary<int, (string, int)> EducationCourses { get; set; }
    }
}
