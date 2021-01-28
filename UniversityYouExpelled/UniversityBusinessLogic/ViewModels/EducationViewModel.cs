using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.ViewModels
{
    [DataContract]
    public class EducationViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        [DisplayName("Клиент")]
        public string FIO { get; set; }
        [DataMember]
        [DisplayName("Год обучения")]
        public int YearED { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DataMember]
        [DisplayName("Стоимость обучения")]
        public decimal CostED { get; set; }
        [DataMember]
        [DisplayName("Статус оплаты")]
        public PayStatus PayStatus { get; set; }
        [DataMember]
        [DisplayName("Осталось оплатить")]
        public decimal Remain { get; set; }
        [DataMember]
        public List<EducationCourseViewModel> EducationCourses { get; set; }
    }
}
