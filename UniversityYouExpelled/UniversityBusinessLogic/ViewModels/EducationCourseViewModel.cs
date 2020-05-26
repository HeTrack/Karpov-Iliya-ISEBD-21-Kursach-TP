﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    [DataContract]
    public class EducationCourseViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? EducationId { get; set; }
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
