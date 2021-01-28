using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.ViewModels
{
    [DataContract]
    public class EducationCourseViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int? EducationID { get; set; }
        [DataMember] 
        public int CourseID { get; set; }
        [DataMember]
        [DisplayName("Название курса")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }
        [DataMember]
        [DisplayName("Дата начала курса")]
        public DateTime DateStart { get; set; }
        [DataMember]
        [DisplayName("Продолжительность курса")]
        public int Duration { get; set; }
        [DataMember]
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
        [DataMember]
        [DisplayName("Статус оплаты")]
        public PayStatus PayStatus { get; set; }
    }
}
