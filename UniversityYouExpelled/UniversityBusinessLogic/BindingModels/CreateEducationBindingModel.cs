using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class CreateEducationBindingModel
    {
        [DataContract]
        public class CreateTravelBindingModel
        {
            [DataMember]
            public int EducationId { get; set; }
            [DataMember]
            public int ClientId { get; set; }
            [DataMember]
            public decimal EducationCost { get; set; }
        }
    }
}
