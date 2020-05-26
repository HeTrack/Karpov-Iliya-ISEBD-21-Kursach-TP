using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BindingModels
{
   public class EducationBindingModel
    {
        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public DateTime EdCreate { get; set; }
        public int YearEd { get; set; }
        public string StatusEducation { get; set; }
        public decimal EdCost { get; set; }        
        public PayStatus Status { get; set; }
       // public bool IsMay { get; set; }
        public List<EducationCourseBindingModel> EducationCourses { get; set; }
        public List<PayBindingModel> Payments { get; set; }
    }
}
