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
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int EducationId { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime PayDate { get; set; }
        [DataMember]
        [DisplayName("Сумма оплаты")]
        public decimal Sum { get; set; }
    }
}
