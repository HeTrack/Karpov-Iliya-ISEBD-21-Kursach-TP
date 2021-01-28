using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BindingModels
{
    public class EducationCourseBindingModel
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public int? EducationID { get; set; }
        [DataMember]
        public int CourseID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Lecturer { get; set; }
        [DataMember]
        public DateTime DateStart { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public PayStatus PayStatus { get; set; }
    }
}
