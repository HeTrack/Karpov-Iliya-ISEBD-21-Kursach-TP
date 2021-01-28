using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    [DataContract]
    public class PayViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int EducationID { get; set; }
        [DataMember]
        public int CourseID { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime DatePay { get; set; }
        [DataMember]
        [DisplayName("Сумма оплаты")]
        public decimal SumPay { get; set; }
    }
}
