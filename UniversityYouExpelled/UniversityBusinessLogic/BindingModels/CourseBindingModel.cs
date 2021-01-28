using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    [DataContract]
    public class CourseBindingModel
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public int Year { get; set; }
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

    }
}
