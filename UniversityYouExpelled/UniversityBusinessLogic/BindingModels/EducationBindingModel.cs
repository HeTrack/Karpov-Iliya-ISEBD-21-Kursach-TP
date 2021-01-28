using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BindingModels
{
    [DataContract]
    public class EducationBindingModel
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int YearED { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public decimal CostED { get; set; }
        [DataMember]
        public PayStatus PayStatus { get; set; }
        public decimal Remain { get; set; }
        public int From;
        public int To;
        [DataMember]
        public List<EducationCourseBindingModel> EducationCourses { get; set; } 
    }
}
