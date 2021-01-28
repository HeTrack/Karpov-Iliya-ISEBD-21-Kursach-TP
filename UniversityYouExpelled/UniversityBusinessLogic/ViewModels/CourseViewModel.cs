using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
   public class CourseViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Год обучения")]
        public int Year { get; set; }
        [DataMember]
        [DisplayName("Название курса")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }
        [DisplayName("Дата начала курса")]
        public DateTime DateStart { get; set; }
        [DisplayName("Продолжительность курса")]
        public string Duration { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
    }
}
