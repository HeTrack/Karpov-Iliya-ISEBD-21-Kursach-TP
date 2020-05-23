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
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("Год обучения")]
        public int YearEd { get; set; }
        [DataMember]
        [DisplayName("Дата создания обучения")]
        public DateTime EducationCreate { get; set; }
        [DataMember]
        [DisplayName("Статус обучения")]
        public string StatusEducation { get; set; }
        [DataMember]
        [DisplayName("Стоимость обучения")]
        public decimal EdCost { get; set; }
        [DataMember]       
        [DisplayName("Статус")]
        public PayStatus Status { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> EducationCourses { get; set; }
    }
}
