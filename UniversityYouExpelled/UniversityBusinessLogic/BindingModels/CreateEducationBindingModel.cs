using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{   
        [DataContract]
        public class CreateEducationBindingModel
        {
            [DataMember]
            public int EducationId { get; set; }
            [DataMember]
            public int ClientId { get; set; }
            [DataMember]
            public decimal EdCost { get; set; }
           // [DataMember]
            //public bool IsMay { get; set; }
        }
    }
