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
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public DateTime EdCreate { get; set; }
        [DataMember]
        public int YearEd { get; set; }
        [DataMember]
        public string StatusEducation { get; set; }
        [DataMember]
        public decimal EdCost { get; set; }
        [DataMember]       
        public decimal Remain { get; set; }
        [DataMember]
        public EducationStatus Status { get; set; }
        [DataMember]
        public List<EducationCourseBindingModel> EducationCourses { get; set; }
    }
}
