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
        public int? Id { get; set; }
        [DataMember]
        public int EducationId { get; set; }
        [DataMember]
        public int ClientId { get; set; }       
        [DataMember]
        public DateTime PayDate{ get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
