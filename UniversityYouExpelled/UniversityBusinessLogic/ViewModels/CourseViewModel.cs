using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    class CourseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название курса")]
        public string CourseName { get; set; }
        [DisplayName("ФИО Преподавателя")]
        public string LecturerFIO { get; set; }
        [DisplayName("Дата начала курса")]
        public DateTime StartCourse { get; set; }       
        [DisplayName("Цена")]
        public decimal Cost { get; set; }
    }
}
