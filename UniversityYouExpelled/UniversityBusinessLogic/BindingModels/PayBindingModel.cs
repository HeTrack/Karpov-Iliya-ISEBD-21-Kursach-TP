using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    [DataContract]
    public class PayBindingModel
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int EducationID { get; set; }
        [DataMember]
        public int CourseID { get; set; }       
        [DataMember]
        public DateTime DatePay { get; set; }
        [DataMember]
        public decimal SumPay { get; set; }
    }
}
