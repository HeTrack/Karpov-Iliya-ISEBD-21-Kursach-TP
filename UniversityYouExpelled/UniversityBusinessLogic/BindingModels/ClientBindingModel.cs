using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.BindingModels
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public string FIO { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public BlockStatus BlockStatus {get; set;}
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime DateRegister { get; set; }
    }
}
