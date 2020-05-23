using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
   public class CourseBindingModel
    {
        public int? Id { get; set; }           
        public string LecturerFIO { get; set; }
        public string CourseName { get; set; }
        public DateTime StartCourse { get; set; }
        public decimal Cost { get; set; }

    }
}
