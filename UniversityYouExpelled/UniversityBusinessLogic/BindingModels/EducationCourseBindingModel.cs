using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class EducationCourseBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? EducationId { get; set; }
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public string LecturerFIO { get; set; }
        [DataMember]
        public string CourseName { get; set; }
        [DataMember]
        public DateTime StartCourse { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
    }
}
